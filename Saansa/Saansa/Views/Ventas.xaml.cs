using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saansa.Views;
using Sharpnado.Presentation.Forms.RenderedViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class Ventas : ContentPage
    {
        public HorizontalListViewLayout ListLayout { get; set; } = HorizontalListViewLayout.Linear;


        private Bebidas update = new Bebidas();
        public Ventas()
        {
            InitializeComponent();
            if (App.listaCarrito != null)
            {
                App.listaCarrito.Clear();
            }
        }


        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (App.listaCarrito != null)
            {
                App.listaCarrito.Clear();
            }
            update.update_local_db();
            Navigation.PushAsync(new Categoria());
        }
    }
}