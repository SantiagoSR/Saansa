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
            Navigation.PushAsync(new NuevaVenta());
        }

        void Medi_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Resposteria_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Bebidas_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Mecato_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void goToCart1_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CarritoDeVentas());
        }
    }
}
