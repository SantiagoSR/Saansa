using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saansa.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ventas : ContentPage
    {
        public Ventas()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NuevaVenta());
        }
    }
}