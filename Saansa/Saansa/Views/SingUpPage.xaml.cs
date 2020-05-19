using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Saansa.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUpPage : ContentPage
    {
        
        public SingUpPage()
        {
            InitializeComponent();
        }


        async private void Guardar_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContraseña.Text) && !string.IsNullOrEmpty(txtCorreo.Text))
            {
                MySqlConnection con = new MySqlConnection(PaginaPrincipal.conexion);
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                        if (string.IsNullOrEmpty(txtTelefono.Text)) { 
                        string sql = "INSERT INTO users(Nombre_Usuario,Contra,Correo) VALUES(@Nombre_Usuario,@Contra,@Correo)";

                        using (MySqlCommand cmd = new MySqlCommand(sql, con)) {
                            cmd.Parameters.AddWithValue("@Nombre_Usuario",txtUsuario.Text);
                            cmd.Parameters.AddWithValue("@Contra", txtContraseña.Text);
                            cmd.Parameters.AddWithValue("@Correo",txtCorreo.Text);
                            //cmd.Parameters.AddWithValue("@Telefono",Convert.ToInt32(txtTelefono.Text));
                            cmd.CommandType = CommandType.Text;

                            int result = cmd.ExecuteNonQuery();
                            if (result < 0) {

                                await DisplayAlert("Paso esto: ", "Error añadiendo usuario", "test");
                            }
                           }
                            await DisplayAlert("Exito", "Se añadio tu usuario", "Ok");
                        }
                        else
                        {
                            string sql = "INSERT INTO users(Nombre_Usuario,Contra,Correo) VALUES(@Nombre_Usuario,@Contra,@Correo,@Telefono)";

                            using (MySqlCommand cmd = new MySqlCommand(sql, con))
                            {
                                cmd.Parameters.AddWithValue("@Nombre_Usuario", txtUsuario.Text);
                                cmd.Parameters.AddWithValue("@Contra", txtContraseña.Text);
                                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                                cmd.Parameters.AddWithValue("@Telefono",Convert.ToInt32(txtTelefono.Text));
                                cmd.CommandType = CommandType.Text;

                                int result = cmd.ExecuteNonQuery();
                                if (result < 0)
                                {

                                    await DisplayAlert("Paso esto: ", "Error añadiendo usuario", "test");
                                }
                            }
                            await DisplayAlert("Exito", "Se añadio tu usuario", "Ok");

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    await DisplayAlert("Paso esto: ", Convert.ToString(ex), "test");
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                await DisplayAlert("Required", "Los campos de Usuario, Contraseña y Correo son necesarios", "OK");
            }

        }
    }
}