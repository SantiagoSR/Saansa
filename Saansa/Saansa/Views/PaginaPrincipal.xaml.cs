﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : ContentPage
    {
        //Publico para que se pueda usar desde cualquier sitio
        public static string conexion ="Server=mi-cafe-delicias.mysql.database.azure.com;Port=3306;database=base_datos_proyecto; User Id = ssantacrur@mi-cafe-delicias;Password=proyectoIntegrador1;charset=utf8";

        private Bebidas update = new Bebidas();
        public PaginaPrincipal()
        {
            InitializeComponent();
        }

        async private void LogIn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContraseña.Text))
            {
                var User = "";
                var contra = "";
                var isAdmin = "";
                var isSeller = "";
                MySqlConnection con = new MySqlConnection(conexion);
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {

                        string sql = "SELECT Nombre_Usuario, Contra, isAdmin, isSeller FROM users WHERE Nombre_Usuario = '" + txtUsuario.Text + "' AND Contra = '" + txtContraseña.Text + "'";
                        MySqlCommand command = new MySqlCommand(sql, con);

                        con.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        int count = 0;
                        while (reader.Read())
                        {
                            User = reader.GetString("Nombre_Usuario");
                            contra = reader.GetString("Contra");
                            isAdmin = reader.GetString("isAdmin");
                            isSeller = reader.GetString("isSeller");
                            count = count + 1;
                        }

                        if (count == 1)
                        {
                            if (isAdmin == "true")
                            {
                                await Navigation.PushAsync(new MainPage());
                                App.nivel = "0";
                                update.update_local_db();
                            }
                            else if (isSeller == "true")
                            {
                                await Navigation.PushAsync(new MyPage());
                                App.nivel = "1";

                                update.update_local_db();
                            }
                            else
                            {

                                App.nivel = "2";
                                await Navigation.PushAsync(new Categoria());

                                update.update_local_db();

                            }

                        }
                        else if (count > 1)

                        {
                            await DisplayAlert("Paso esto: ", "Usuario y Contraseña duplicados", "Ok");
                        }
                        else
                        {
                            await DisplayAlert("Paso esto: ", "Usuario ó Contraseña no validos", "Ok");
                        }


                    }
                }
                catch (MySqlException ex)
                {
                    await DisplayAlert("Paso esto: ", Convert.ToString(ex), "Ok");
                }
                finally
                { 
                    con.Close();
                }
            }
            else {
                await DisplayAlert("Campos requeridos: ", "Usuario y/o Contraseña vacia", "Ok");
            }
        }

        private void SingUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingUpPage());
        }
    }
}