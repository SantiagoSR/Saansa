using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FreshMvvm;
using Saansa.ModeloPaginas;

namespace Saansa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var page = FreshPageModelResolver.ResolvePageModel<ModeloListaPaginaArticulo>();
            var navContainer = new FreshNavigationContainer(page);
            MainPage = navContainer;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
