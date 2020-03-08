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
                    Producto = txtNombre.Text
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
                await DisplayAlert("Required", "Please Enter name!", "OK");
            }
        }



        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                //Get Person
                var articulo = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtProducto.Text));
                if (articulo != null)
                {
                    txtNombre.Text = articulo.Producto;
                    await DisplayAlert("Success", "Person Name: " + articulo.Producto, "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter PersonID", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProducto.Text))
            {
                Modelos.Articulo articulo = new Modelos.Articulo()
                {
                    Cantidad = Convert.ToInt32(txtProducto.Text),
                    Producto = txtNombre.Text
                };

                //Update Person
                await App.SQLiteDb.SaveItemAsync(articulo);

                txtProducto.Text = string.Empty;
                txtNombre.Text = string.Empty;
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
                await DisplayAlert("Required", "Please Enter PersonID", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProducto.Text))
            {
                //Get Person
                var person = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtProducto.Text));
                if (person != null)
                {
                    //Delete Person
                    await App.SQLiteDb.DeleteItemAsync(person);
                    txtProducto.Text = string.Empty;
                    await DisplayAlert("Success", "Person Deleted", "OK");

                    //Get All Persons
                    var personList = await App.SQLiteDb.GetItemsAsync();
                    if (personList != null)
                    {
                        lstArticulo.ItemsSource = personList;
                    }
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter PersonID", "OK");
            }
        }



    }
}