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
    }
}
