﻿using System;
using System.IO;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Saansa.Modelos;
using System.Collections.Generic;
using Saansa.Views;

namespace Saansa
{
    public partial class App : Application
    {

        public static IList<ArticuloCarrito> listaCarrito;
        static SQLiteHelper db;
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new PaginaPrincipal());

        }
        public static SQLiteHelper SQLiteDb
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Inventario.db3"));
                }
                return db;
            }
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
