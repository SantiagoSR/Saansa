using System;
using System.Collections.Generic;
using Saansa.Views;
using Xamarin.Forms;

namespace Saansa
{
    public partial class MyPage : TabbedPage
    {
        private string ayudante = "1";
        public MyPage()
        {
            InitializeComponent();
            new Categoria();
        }
    }
}
