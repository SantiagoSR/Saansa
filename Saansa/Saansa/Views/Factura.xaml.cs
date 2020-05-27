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
    }
}