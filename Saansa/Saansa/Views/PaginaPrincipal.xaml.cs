using System;
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
                MySqlConnection con = new MySqlConnection(conexion);
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {

                        string sql = "SELECT Nombre_Usuario, Contra FROM users WHERE Nombre_Usuario = '" + txtUsuario.Text + "' AND Contra = '" + txtContraseña.Text + "'";
                        MySqlCommand command = new MySqlCommand(sql, con);

                        con.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        int count = 0;
                        while (reader.Read())
                        {
                            User = reader.GetString("Nombre_Usuario");
                            contra = reader.GetString("Contra");
                            count = count + 1;
                        }

                        if (count == 1)
                        {
                            await Navigation.PushAsync(new MainPage());
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