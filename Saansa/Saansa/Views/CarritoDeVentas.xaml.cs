using System;
using System.Collections.Generic;
using System.Collections;
using SQLite;

using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class CarritoDeVentas : ContentPage
    {
        public CarritoDeVentas()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
