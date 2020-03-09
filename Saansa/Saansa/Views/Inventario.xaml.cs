using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saansa.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inventario : ContentPage
    {
        public Inventario()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Get All Persons
            var articuloLista = await App.SQLiteDb.GetItemsAsync();
            if (articuloLista != null)
            {
                lstArticulo.ItemsSource = articuloLista;
            }
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                Modelos.Articulo articulo = new Modelos.Articulo()
                {   
                    Producto = txtNombre.Text,
                    Id = Convert.ToInt32(txtCantidad.Text)
                };

                //Add New Person
                await App.SQLiteDb.SaveItemAsync(articulo);
                txtNombre.Text = string.Empty;
                await DisplayAlert("Success", "Articulo añadido con exito", "OK");
                //Get All Persons
                var articuloLista = await App.SQLiteDb.GetItemsAsync();
                if (articuloLista != null)
                {
                    lstArticulo.ItemsSource = articuloLista;
                }
            }
            else
            {
                await DisplayAlert("Required", "Ingresa un Nombre!", "OK");
            }
        }



        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                //Get Person
                //var articulo = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtProducto.Text));
                var articulo = await App.SQLiteDb.GetItemAsync(txtNombre.Text);
                if (articulo != null)
                {
                    txtNombre.Text = articulo.Producto;
                    await DisplayAlert("Success", "Nombre Articulo: " + articulo.Producto + " Cantidad:" + articulo.Id , "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Ingresa Identificacion", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            //txtProducto
            if (!string.IsNullOrEmpty(txtProducto.Text))
            {
                Modelos.Articulo articulo = new Modelos.Articulo()
                {
                    //Cantidad = Convert.ToInt32(txtProducto.Text),
                    Producto = txtNombre.Text,
                    Id  = Convert.ToInt32(txtCantidad.Text)
                };

                //Update Person
                await App.SQLiteDb.SaveItemAsync(articulo);

                txtProducto.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtCantidad.Text = string.Empty;

                await DisplayAlert("Success", "Articulo Actualizado", "OK");
                //Get All Persons
                var articuloLista = await App.SQLiteDb.GetItemsAsync();
                if (articuloLista != null)
                {
                    lstArticulo.ItemsSource = articuloLista;
                }

            }
            else
            {
                await DisplayAlert("Required", "Ingresa el Nombre del articulo", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {   //txtProducto
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                //Get Person
                //var articulo = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtProducto.Text));
                var articulo = await App.SQLiteDb.GetItemAsync(txtNombre.Text);

                if (articulo != null)
                {
                    //Delete Person
                    await App.SQLiteDb.DeleteItemAsync(articulo);
                    //txtProducto
                    txtNombre.Text = string.Empty;
                    await DisplayAlert("Success", "Articulo Borrado", "OK");

                    //Get All Persons
                    var ListaArticulos = await App.SQLiteDb.GetItemsAsync();
                    if (ListaArticulos != null)
                    {
                        lstArticulo.ItemsSource = ListaArticulos;
                    }
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Id Articulo", "OK");
            }
        }



    }
}