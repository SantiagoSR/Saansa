using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class NuevaVenta : ContentPage
    {
        public NuevaVenta()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var articuloLista = await App.SQLiteDb.GetItemsAsync();
            if (articuloLista != null) {
                listART.ItemsSource = articuloLista;
            }
        }
    }
}
