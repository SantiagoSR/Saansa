using System;
using System.Collections.Generic;
using System.Collections;
using SQLite;

using Xamarin.Forms;
using Saansa.Modelos;

namespace Saansa.Views
{
    public partial class CarritoDeVentas : ContentPage
    {
        public string strQR;
        public CarritoDeVentas()
        {
            InitializeComponent();
            listaArticulosCarrito.ItemsSource = App.listaCarrito;
            int price = 0;
            this.strQR = "";
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

                    await App.SQLiteDb.SaveItemAsync(articulo);
                }

            }

            await Navigation.PushAsync(new MainPage());
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
