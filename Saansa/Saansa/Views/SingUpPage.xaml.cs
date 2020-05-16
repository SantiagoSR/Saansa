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
                MySqlConnection con = new MySqlConnection("Server=mi-cafe-delicias.mysql.database.azure.com;Port=3306;database=base_datos_proyecto; User Id = ssantacrur@mi-cafe-delicias;Password=proyectoIntegrador1;charset=utf8");
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open(); 
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO users(Nombre_Usuario,Contra,Correo) VALUES(@user,@pass,@correo)", con);
                        cmd.Parameters.AddWithValue("@user", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@pass", txtContraseña.Text);
                        cmd.Parameters.AddWithValue("@correo",txtCorreo.Text);
                        cmd.Parameters.AddWithValue("@telefono",Convert.ToInt32(txtTelefono.Text));
                        cmd.ExecuteNonQuery();
                        await DisplayAlert("CHIDO", "Se añadio tu usuario", "Wiiii");
                    }
                }
                catch (MySqlException ex)
                {
                    await DisplayAlert("Paso esto: ", Convert.ToString(ex), "");
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