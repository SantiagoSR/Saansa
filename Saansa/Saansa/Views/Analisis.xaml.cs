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
                if (App.listaAnalisis != null)
                {
                    App.listaAnalisis.Clear();
                    App.listaAnalisis = articuloLista.OrderBy(a => a.VecesVendidas).ToList();
                    productosMasVendidos.ItemsSource = App.listaAnalisis;
                    productosMenosVendidos.ItemsSource = articuloLista.OrderByDescending(a => a.VecesVendidas).ToList();
                }
                else {
                    App.listaAnalisis = articuloLista.OrderBy(a => a.VecesVendidas).ToList();
                    productosMasVendidos.ItemsSource = App.listaAnalisis;
                    productosMenosVendidos.ItemsSource = articuloLista.OrderByDescending(a => a.VecesVendidas).ToList();
                }
            }
        }


    }
}
