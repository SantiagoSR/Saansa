using System;
using System.Collections.Generic;
using System.Linq;
using Saansa.Modelos;
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
            var articuloLista = await App.SQLiteDb.GetItemsAsync();
            if (articuloLista != null)
            {
                productosMasVendidos.ItemsSource = articuloLista.OrderBy(a => a.VecesVendidas).ToList();
                productosMenosVendidos.ItemsSource = articuloLista.OrderByDescending(a => a.VecesVendidas).ToList();
            }
        }


    }
}
