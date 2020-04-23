using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class Categoria : ContentPage
    {
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
            Navigation.PushAsync(new NuevaVenta("Otros"));
        }

        void Medi_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Medicamentos"));
        }

        void Resposteria_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Reposteria"));
        }

        void Bebidas_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Bebidas"));
        }

        void Mecato_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta("Mecato"));
        }

        async void goToCart1_Clicked_1(System.Object sender, System.EventArgs e)
        {
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
