using Saansa.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaInventario : ContentPage
        
    {
        string c;
        public VistaInventario(string category)
        {
            InitializeComponent();
            //App.listainventario = new List<Modelos.Articulo>();
            this.c = category;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Get All Products
            var articuloLista = await App.SQLiteDb.GetItemsAsyncCategory(c);
            if (articuloLista != null)
            {
                MyList.ItemsSource = articuloLista;
            }
        }

        private async void My_List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ayudante = e.Item as Modelos.Articulo;
            Modelos.Articulo detalles = ayudante;
           await DisplayAlert("Nombre: " + detalles.Producto, "\nIdentificador:" + detalles.Id + 
                "\nCantidad: " + detalles.Cantidad + "\nPrecio: " + detalles.Precio,"OK");

        }
        
    }
}