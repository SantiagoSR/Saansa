using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FreshMvvm;

namespace Saansa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaArticulo : FreshBaseContentPage
    {
        public PaginaArticulo()
        {
            InitializeComponent();
        }
    }
}