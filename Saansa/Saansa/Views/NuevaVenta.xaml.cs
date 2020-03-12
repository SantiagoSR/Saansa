using System;
using System.Collections.Generic;
using SQLite;
using System.Diagnostics;

using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class NuevaVenta : ContentPage
    {
        public NuevaVenta()
        {
            InitializeComponent();
            App.listaCarrito = new List<Modelos.ArticuloCarrito>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var articuloLista = await App.SQLiteDb.GetItemsAsync();
            if (articuloLista != null)
            {
                listART.ItemsSource = articuloLista;
            }
        }

        private int pQuantity = 0;
        void addButton_Clicked(System.Object sender, System.EventArgs e)
        {
            pQuantity++;
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[2];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            productQuantity.Text = pQuantity.ToString();
        }

        private async void addCart_Clicked(System.Object sender, System.EventArgs e)
        {
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[2];
            Entry productQuantity = (Entry)stacklayout1.Children[0];

            StackLayout stacklayout2 = (StackLayout)parentstacklayout.Children[0];
            Label nombreProducto = (Label)stacklayout2.Children[0];


            if (!string.IsNullOrEmpty(productQuantity.Text) && !productQuantity.Text.Equals("0"))
            {
                App.listaCarrito.Add(new Modelos.ArticuloCarrito
                {
                    Producto = nombreProducto.Text,
                    Cantidad = Convert.ToInt16(productQuantity.Text)
                }); 
            }
            else
            {
                await DisplayAlert("No hay cantidad", "Necesitas ingresar al menos un producto para poder ingresar al carrito", "OK");
            }
        }

        void goToCart_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CarritoDeVentas());
        }

        private void subButton_Clicked(System.Object sender, System.EventArgs e)
        {
            pQuantity--;
            if (pQuantity == -1)
            {
                pQuantity = 0;
            }
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[2];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            productQuantity.Text = pQuantity.ToString();
        }
    }
}
