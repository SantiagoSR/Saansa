using Saansa.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scanner : ContentPage
    {
        public Scanner()
        {
            InitializeComponent();

            BindingContext = new ScannerModel(this.Navigation);
        }
    }
}