using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class Analisis : ContentPage
    {
        public Analisis()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var articuloLista = await App.SQLiteDb.GetItemsAsyncCategory(c);
            if (articuloLista != null)
            {
                productosMasVendidos.ItemsSource = articuloLista;
            }
        }


    }
}
