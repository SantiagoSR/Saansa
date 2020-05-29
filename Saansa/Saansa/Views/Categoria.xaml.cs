using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class Categoria : ContentPage
    {
        List<Modelos.ArticuloCarrito> carrito = new List<Modelos.ArticuloCarrito>();
       
        public Categoria()
        {
            InitializeComponent();
            if (App.listaCarrito != null)
            {
                App.listaCarrito.Clear();
            }
        }

        void Otros_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Otros", carrito));
        }

        void Medi_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Medicamentos", carrito));
        }

        void Resposteria_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Reposteria", carrito));
        }

        void Bebidas_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Bebidas", carrito));
        }

        void Mecato_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Mecato", carrito));
        }

        async void goToCart1_Clicked_1(System.Object sender, System.EventArgs e)
        {
            int ayudante = App.listaCarrito.Count;
            if (App.listaCarrito != null)
            {
                await Navigation.PushAsync(new CarritoDeVentas());
            }
            else {
                await DisplayAlert("Necesario", "Tienes que agregar productos al carrito para poder ir a él.", "OK");
            }
        }
    }
}
