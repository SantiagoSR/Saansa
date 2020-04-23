using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saansa.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Saansa.Views;

namespace Saansa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inventario : ContentPage
    {
        public Inventario()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtProducto.Text))
            {
                Modelos.Articulo articulo = new Modelos.Articulo()
                {
                    Id = txtProducto.Text,
                    Producto = txtNombre.Text,
                    Precio = Convert.ToInt32(txtPrecio.Text),
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                    MasterCategory = txtMainCategory.Text,
                    Category1 = txtSub1.Text,
                    Category2 = txtSub2.Text,
                    Category3 = txtSub3.Text
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
            }
            
            else
            {
                await DisplayAlert("Required", "Ingresa un Codigo", "OK");
            }
        }



        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                //Get Person
                //var articulo = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtProducto.Text))
                var articulo = await App.SQLiteDb.GetItemAsync(txtNombre.Text);
                if (articulo != null)
                {
                    txtNombre.Text = articulo.Producto;
                    await DisplayAlert("Success", "Identificacion: " + articulo.Id + "\nNombre Articulo: " + articulo.Producto + "\nCantidad: " + articulo.Cantidad + "\nPrecio: " + articulo.Precio
                        + "\nCategoria: " + articulo.MasterCategory, "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Ingresa Nombre del Articulo", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                Modelos.Articulo articulo = await App.SQLiteDb.GetItemAsync(txtNombre.Text);

                //Aparentemente dejar el texto vacío no es equivalente a "", por eso el +"0"
                articulo.Cantidad = (string.Equals("0", txtCantidad.Text + "0")) ? articulo.Cantidad : Convert.ToInt32(txtCantidad.Text);
                articulo.Category1 = (string.Equals("0", txtSub1.Text + "0")) ? articulo.Category1 : txtSub1.Text;
                articulo.Category2 = (string.Equals("0", txtSub2.Text + "0")) ? articulo.Category2 : txtSub2.Text;
                articulo.Category3 = (string.Equals("0", txtSub3.Text + "0")) ? articulo.Category3 : txtSub3.Text;
                articulo.MasterCategory = (string.Equals("0", txtMainCategory.Text + "0")) ? articulo.MasterCategory : txtMainCategory.Text;
                articulo.Precio = (string.Equals("0", txtPrecio.Text + "0")) ? articulo.Precio : Convert.ToInt32(txtPrecio.Text);
                articulo.Producto = (string.Equals("0", txtProducto.Text + "0")) ? articulo.Producto : txtNombre.Text;

                await App.SQLiteDb.SaveItemAsync(articulo);

                txtNombre.Text = string.Empty;
                txtCantidad.Text = string.Empty;
                txtProducto.Text = string.Empty;
                txtPrecio.Text = string.Empty;
                txtMainCategory.Text = string.Empty;
                txtSub1.Text = string.Empty;
                txtSub2.Text = string.Empty;
                txtSub3.Text = string.Empty;
                await DisplayAlert("Success", "Producto actualizado con éxito", "OK");
                //Get All Persons
                /*var articuloLista = await App.SQLiteDb.GetItemsAsync();
                if (articuloLista != null)
                {
                    lstArticulo.ItemsSource = articuloLista;
                }
                */

            }
            else
            {
                await DisplayAlert("Required", "Please Enter código articulo", "OK");
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
                  /*  var ListaArticulos = await App.SQLiteDb.GetItemsAsync();
                    if (ListaArticulos != null)
                    {
                        lstArticulo.ItemsSource = ListaArticulos;
                    }
                    */
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Id Articulo", "OK");
            }
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VistaInventario());
        }


    }
}