using System;
using System.Collections.Generic;
using SQLite;
using System.Diagnostics;

using Xamarin.Forms;

namespace Saansa.Views
{
    public partial class NuevaVenta : ContentPage
    {
        string c;
        public NuevaVenta(string category)
        {
            InitializeComponent();
            App.listaCarrito = new List<Modelos.ArticuloCarrito>();
            c = category;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var articuloLista = await App.SQLiteDb.GetItemsAsyncCategory(c);
            if (articuloLista != null)
            {
                listART.ItemsSource = articuloLista;
            }
        }

        public static async void crearListaConQR(string strQR)
        {
            string id = "";
            string cantidad = "";
            char actual = ' ';
            char anterior = ' ';
            Modelos.Articulo articulo;

            App.listaCarrito = new List<Modelos.ArticuloCarrito>();

            //Recorrer el texto leído del QR, agregando cada artículo al carrito
            for (int i = 0; i < strQR.Length; i++)
            {
                actual = strQR[i];
                //Revisar si es número (para saber si el caracter hace parte de la cantidad o del Id)
                if (actual < 48 || actual > 57)
                {
                    //Revisar si el caracter anterior era número y si sí, ya se está leyendo un nuevo producto
                    if (anterior >= 48 && anterior <= 57)
                    {
                        //Agregar el producto al carrito
                        articulo = await App.SQLiteDb.GetItemAsyncCodigo(id);

                        App.listaCarrito.Add(new Modelos.ArticuloCarrito
                        {
                            Producto = articulo.Producto,
                            Cantidad = Convert.ToInt16(cantidad),
                            Id = articulo.Id
                        });

                        //Reset id y cantidad porque ya serán para el nuevo producto
                        id = "" + actual;
                        cantidad = "";
                    }
                    else
                    {
                        //Si el caracter anterior no es número, hace parte aún del id del producto actual
                        //Agregar el caracter actual al string de id
                        id += actual;
                    }
                }
                else
                {
                    //Si el caracter actual es un número, ya terminó de leer el id y empezó a leer la cantidad
                    //Agregar el caracter actual al string 'cantidad'
                    cantidad += actual;
                }
                //anterior = actual para la siguiente iteración
                anterior = actual;
            }

            //Agregar el último producto leído al carrito
            articulo = await App.SQLiteDb.GetItemAsyncCodigo(id);

            App.listaCarrito.Add(new Modelos.ArticuloCarrito
            {
                Producto = articulo.Producto,
                Cantidad = Convert.ToInt16(cantidad),
                Id = articulo.Id
            });

        }

        private int pQuantity = 0;
        void addButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[3];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            pQuantity = Convert.ToInt16(productQuantity.Text);
            pQuantity++;
            productQuantity.Text = pQuantity.ToString();
        }

        private async void addCart_Clicked(System.Object sender, System.EventArgs e)
        {
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[3];
            Entry productQuantity = (Entry)stacklayout1.Children[0];

            StackLayout stackLayout2 = (StackLayout)parentstacklayout.Children[1];
            Label precioProducto = (Label)stackLayout2.Children[0];

            StackLayout stacklayout3 = (StackLayout)parentstacklayout.Children[0];
            Label nombreProducto = (Label)stacklayout3.Children[0];


            if (!string.IsNullOrEmpty(productQuantity.Text) && !productQuantity.Text.Equals("0"))
            {
                var articulo = await App.SQLiteDb.GetItemAsync(nombreProducto.Text);
                pQuantity = 0;
                //Creamos el nuevo articulo del carrito
                Modelos.ArticuloCarrito nuevo = new Modelos.ArticuloCarrito { Producto = nombreProducto.Text,
                    Cantidad = Convert.ToInt16(productQuantity.Text),
                    Precio = Convert.ToInt16(precioProducto.Text) * Convert.ToInt16(productQuantity.Text),
                    Id = articulo.Id

                };
                //Vemos si el articulo ya fue creado y está en la lista del carrito.
                bool containsItem = false; 
                foreach (Modelos.ArticuloCarrito a in App.listaCarrito)
                {
                    if (string.Compare(a.Producto, nuevo.Producto) == 0)
                    {
                        containsItem = true;
                    }
                }
                Console.WriteLine(containsItem);

                if (containsItem)
                {   //Recorremos la lista del carrito para encontrar el producto y cambiar su cantidad.
                    foreach (Modelos.ArticuloCarrito a in App.listaCarrito)
                    {
                        if (string.Compare(a.Producto, nuevo.Producto) == 0)
                        {
                            a.Cantidad = nuevo.Cantidad;
                            a.Precio = nuevo.Precio;
                        }
                    }
                }
                else {
                    App.listaCarrito.Add(nuevo);
                }
            }
            else
            {
                await DisplayAlert("No hay cantidad", "Necesitas ingresar al menos un producto para poder ingresar al carrito", "OK");
            }
        }

        void goToCart_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CarritoDeVentas());
        }

        private void subButton_Clicked(System.Object sender, System.EventArgs e)
        {
            
            var buttonClickHandler = (Button)sender;
            StackLayout parentstacklayout = (StackLayout)buttonClickHandler.Parent;
            StackLayout stacklayout1 = (StackLayout)parentstacklayout.Children[3];
            Entry productQuantity = (Entry)stacklayout1.Children[0];
            pQuantity = Convert.ToInt16(productQuantity.Text);
            pQuantity--;
            if (pQuantity == -1)
            {
                pQuantity = 0;
            }
            productQuantity.Text = pQuantity.ToString();
        }


    }
}
