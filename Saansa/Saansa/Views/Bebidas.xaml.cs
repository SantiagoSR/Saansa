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
    public partial class Bebidas : ContentPage
    {
        public Bebidas()
        {
            InitializeComponent();
            
        }
        
        void Otros_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VistaInventario("Otros"));
        }

        void Medi_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VistaInventario("Medicamentos"));
        }

        void Resposteria_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VistaInventario("Reposteria"));
        }

        void Liquidos_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VistaInventario("Bebidas"));
        }

        void Mecato_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VistaInventario("Mecato"));
        }

       
    }
}