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
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[3];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            pQuantity = Convert.ToInt16(productQuantity.Text);
            pQuantity++;
            productQuantity.Text = pQuantity.ToString();
        }

        private async void addCart_Clicked(System.Object sender, System.EventArgs e)
        {
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[3];
            Entry productQuantity = (Entry)stacklayout1.Children[0];

            StackLayout stackLayout2 = (StackLayout)parentstacklayout.Children[1];
            Label precioProducto = (Label)stackLayout2.Children[0];

            StackLayout stacklayout3 = (StackLayout)parentstacklayout.Children[0];
            Label nombreProducto = (Label)stacklayout3.Children[0];


            if (!string.IsNullOrEmpty(productQuantity.Text) && !productQuantity.Text.Equals("0"))
            {
                pQuantity = 0;
                //Creamos el nuevo articulo del carrito
                Modelos.ArticuloCarrito nuevo = new Modelos.ArticuloCarrito { Producto = nombreProducto.Text,
                    Cantidad = Convert.ToInt16(productQuantity.Text),
                    Precio = Convert.ToInt16(precioProducto.Text) * Convert.ToInt16(productQuantity.Text)
                };
                //Vemos si el articulo ya fue creado y está en la lista del carrito.
                bool containsItem = false; 
                foreach (Modelos.ArticuloCarrito a in App.listaCarrito)
                {
                    if (string.Compare(a.Producto, nuevo.Producto) == 0)
                    {
                        containsItem = true;
                    }
                }
                Console.WriteLine(containsItem);

                if (containsItem)
                {   //Recorremos la lista del carrito para encontrar el producto y cambiar su cantidad.
                    foreach (Modelos.ArticuloCarrito a in App.listaCarrito)
                    {
                        if (string.Compare(a.Producto, nuevo.Producto) == 0)
                        {
                            a.Cantidad = nuevo.Cantidad;
                        }
                    }
                }
                else {
                    App.listaCarrito.Add(nuevo);
                }
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
            
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[3];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            pQuantity = Convert.ToInt16(productQuantity.Text);
            pQuantity--;
            if (pQuantity == -1)
            {
                pQuantity = 0;
            }
            productQuantity.Text = pQuantity.ToString();
        }


    }
}
