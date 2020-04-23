using System;
using System.Collections.Generic;
using System.Collections;
using SQLite;

using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class CarritoDeVentas : ContentPage
    {
        public CarritoDeVentas()
        {
            InitializeComponent();
            listaArticulosCarrito.ItemsSource = App.listaCarrito;
            int price = 0;
            foreach (Modelos.ArticuloCarrito a in App.listaCarrito) {
                price += a.Precio;
            }
        }

        private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            if (App.listaCarrito != null)
            {
                foreach (Modelos.ArticuloCarrito a in App.listaCarrito) {
                    Console.WriteLine(a.Producto);
                    Modelos.Articulo articulo = await App.SQLiteDb.GetItemAsync(a.Producto);
                    Console.WriteLine(articulo.Cantidad);
                    articulo.Cantidad = articulo.Cantidad - a.Cantidad;
                    Console.WriteLine(articulo.Cantidad);
                }

            }

            await Navigation.PushAsync(new MainPage());
        }
    }
}
