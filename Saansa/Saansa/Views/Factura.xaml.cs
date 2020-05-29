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
    public partial class Factura : ContentPage
    {
        public Factura(string texto)
        {
            InitializeComponent();

            BindingContext = this;
            label.Text = texto;
           

        }

        void Main_Menu_Clicked (System.Object sender, System.EventArgs e)
        {
            if (App.listaCarrito != null)
            {
                App.listaCarrito.Clear();
            }
            if (App.nivel == "0")
            {
                Navigation.PushAsync(new MainPage());
            }else if (App.nivel == "1")
            {
                Navigation.PushAsync(new MyPage());

            }
            else
            {
                Navigation.PushAsync(new Categoria());

            }
        }
    }
}