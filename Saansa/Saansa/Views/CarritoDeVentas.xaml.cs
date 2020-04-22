using System;
using System.Collections.Generic;
using System.Collections;
using SQLite;

using Xamarin.Forms;
using Saansa.Modelos;
using System.Diagnostics;

namespace Saansa.Views
{
    public partial class CarritoDeVentas : ContentPage
    {
        public string strQR;
        public CarritoDeVentas()
        {
            InitializeComponent();
            listaArticulosCarrito.ItemsSource = App.listaCarrito;
            this.strQR = "";
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
