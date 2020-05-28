using System;
using System.Collections.Generic;
using System.Collections;
using SQLite;
using Xamarin.Forms;
using Saansa.Modelos;
using MySql.Data.MySqlClient;
using System.Data;
namespace Saansa.Views
{
    public partial class CarritoDeVentas : ContentPage
    {
        public string strQR;

        private MySqlConnection con = new MySqlConnection(PaginaPrincipal.conexion);

        public CarritoDeVentas()
        {
            InitializeComponent();
            listaArticulosCarrito.ItemsSource = App.listaCarrito;
            int price = 0;
            this.strQR = "";
            foreach (Modelos.ArticuloCarrito a in App.listaCarrito) {
                price += a.Precio;
            }
            TotalPrice.Text = price.ToString();
        }

        private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            if (App.listaCarrito != null)
            {
                string factura = "";
                double total = 0;
                factura += "\n\nFecha:\t\t" + DateTime.Now.AddHours(-5).ToString("dd-MM-yyyy hh:mm:ss tt")+ "\n";
                factura += "\nProductos";

                try
                {

                    if (con.State == ConnectionState.Closed)
                    {

                        foreach (Modelos.ArticuloCarrito a in App.listaCarrito)
                        {
                            Modelos.Articulo articulo = await App.SQLiteDb.GetItemAsync(a.Producto);

                            articulo.Cantidad = articulo.Cantidad - a.Cantidad;
                            factura += "\n\n\t\t\t" + a.Producto + " (" + a.Cantidad + " x $" + articulo.Precio + ")" + "\t\t\t\t$" + a.Cantidad * articulo.Precio;
                            total += a.Cantidad * articulo.Precio;

                            articulo.VecesVendidas = articulo.VecesVendidas + a.Cantidad;

                            string sql = "UPDATE articulos SET Producto='" + Convert.ToString(articulo.Producto) + "',Cantidad='" + Convert.ToInt32(articulo.Cantidad) + "', Popularidad='" + Convert.ToInt32(articulo.VecesVendidas) + "' WHERE Producto = '" + articulo.Producto + "'";
                            using (MySqlCommand cmd = new MySqlCommand(sql, con))
                            {
                                con.Open();
                                MySqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                }
                                await App.SQLiteDb.SaveItemAsync(articulo);
                            }
                        }
                    }
                }catch (MySqlException ex)
                {
                    await DisplayAlert("Paso esto: ", Convert.ToString(ex) + "Fallo en conexion Intentelo mas tarde", "test");
                }
                finally
                {
                    con.Close();
                }

                factura += "\n\nTOTAL:\t\t$" + total;

                App.listaCarrito.Clear();

                await Navigation.PushAsync(new Factura(factura));
            }
        }
    

        void generateQR_Clicked(System.Object sender, System.EventArgs e)
        {
            //Iterar sobre los artículos de la lista
            foreach (ArticuloCarrito articulo in App.listaCarrito)
            {
                this.strQR += articulo.Id + articulo.Cantidad.ToString();
            }

            var generator = new QRGenerator(this.strQR);
            Navigation.PushAsync(new QRDisplay(this.strQR));
        }
    }
}
