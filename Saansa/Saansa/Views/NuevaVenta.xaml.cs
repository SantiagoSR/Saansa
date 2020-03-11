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
            if (articuloLista != null)
            {
                listART.ItemsSource = articuloLista;
            }
        }

        private int pQuantity = 0;



        void addButton_Clicked(System.Object sender, System.EventArgs e)
        {
            pQuantity++;
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[2];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            productQuantity.Text = pQuantity.ToString();
        }

        private void addCart_Clicked(System.Object sender, System.EventArgs e)
        {

        }

        void goToCart_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CarritoDeVentas());
        }

        private void subButton_Clicked(System.Object sender, System.EventArgs e)
        {
            pQuantity--;
            if (pQuantity == -1)
            {
                pQuantity = 0;
            }
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[2];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            productQuantity.Text = pQuantity.ToString();
        }
    }
}
