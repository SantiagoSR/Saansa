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

            //Get All Products
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
                    Precio  = Convert.ToInt32(txtPrecio.Text),
                    Id = Convert.ToInt32(txtCantidad.Text),
                    MasterCategory = txtMainCategory.Text,
                    Category1   = txtSub1.Text,
                    Category2   = txtSub2.Text,
                    Category3   = txtSub3.Text
                };

                //Add New Person
                await App.SQLiteDb.SaveItemAsync(articulo);
                txtNombre.Text = string.Empty;
                txtCantidad.Text = string.Empty;
                txtProducto.Text = string.Empty;
                txtPrecio.Text = string.Empty;
                txtMainCategory.Text = string.Empty;
                txtSub1.Text = string.Empty;
                txtSub2.Text = string.Empty;
                txtSub3.Text = string.Empty;
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
                    await DisplayAlert("Success", "Identificacion: "+ articulo.Cantidad + "\nNombre Articulo: " + articulo.Producto + "\nCantidad: " + articulo.Id + "\nPrecio: "+ articulo.Precio
                        + "\nCategoria: " +articulo.MasterCategory, "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Ingresa Nombre del Articulo", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProducto.Text))
            {
                Modelos.Articulo articulo = new Modelos.Articulo()
                {
                    Cantidad = Convert.ToInt32(txtProducto.Text),
                    Producto = txtNombre.Text,
                    Precio = Convert.ToInt32(txtPrecio.Text),
                    Id = Convert.ToInt32(txtCantidad.Text),
                    MasterCategory = txtMainCategory.Text,
                    Category1 = txtSub1.Text,
                    Category2 = txtSub2.Text,
                    Category3 = txtSub3.Text
                };

                //Update Person
                await App.SQLiteDb.SaveItemAsync(articulo);

                txtNombre.Text = string.Empty;
                txtCantidad.Text = string.Empty;
                txtProducto.Text = string.Empty;
                txtPrecio.Text = string.Empty;
                txtMainCategory.Text = string.Empty;
                txtSub1.Text = string.Empty;
                txtSub2.Text = string.Empty;
                txtSub3.Text = string.Empty;
                await DisplayAlert("Success", "Person Updated Successfully", "OK");
                //Get All Persons
                var articuloLista = await App.SQLiteDb.GetItemsAsync();
                if (articuloLista != null)
                {
                    lstArticulo.ItemsSource = articuloLista;
                }

            }
            else
            {
                await DisplayAlert("Required", "Please Enter Nombre articulo", "OK");
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
                    txtProducto.Text = string.Empty;
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