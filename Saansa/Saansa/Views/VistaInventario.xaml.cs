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
        public VistaInventario()
        {
            InitializeComponent();
            //App.listainventario = new List<Modelos.Articulo>();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Get All Products
            var articuloLista = await App.SQLiteDb.GetItemsAsync();
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
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Bebidas());
        }
    }
}