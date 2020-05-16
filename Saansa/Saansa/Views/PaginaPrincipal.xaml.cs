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
        public static string conexion ="Server=mi-cafe-delicias.mysql.database.azure.com;Port=3306;database=base_datos_proyecto; User Id = ssantacrur@mi-cafe-delicias;Password=proyectoIntegrador1;charset=utf8";
        public PaginaPrincipal()
        {
            InitializeComponent();
        }

        async private void LogIn_Clicked(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(conexion);
            try
            { 
            
                if(con.State == ConnectionState.Closed)
                {
                con.Open();
                    await DisplayAlert("Paso esto: ","Se Conecto A la base de datos", "Wiiii");
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

        private void SingUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingUpPage());
        }
    }
}