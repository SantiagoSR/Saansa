using MySql.Data.MySqlClient;
using System;
using System.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Saansa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bebidas : ContentPage
    {
        private string producto, categoria_general, subcat1, subcat2, subcat3;
        private int cantidad, precio, costo, veces_vendidas;

        private MySqlConnection con = new MySqlConnection(PaginaPrincipal.conexion);
        public Bebidas()
        {
            InitializeComponent();
            update_local_db();

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

        public async void update_local_db()
        {
            string id;

            try
            {
                if (con.State == ConnectionState.Closed)
                {


                    string sql = "SELECT Id,Producto,Cantidad,Precio,Costo,Categoria_General,SubCategoria1,SubCategoria2,SubCategoria3,Popularidad FROM articulos";
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {

                        con.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            id = reader["Id"].ToString();
                            producto = reader.GetString("Producto");
                            cantidad = reader.GetInt32("Cantidad");
                            precio = reader.GetInt32("Precio");
                            costo = reader.GetInt32("Costo");
                            categoria_general = reader.GetString("Categoria_General");
                            subcat1 = reader.GetString("SubCategoria1");
                            subcat2 = reader.GetString("SubCategoria2");
                            subcat3 = reader.GetString("SubCategoria3");
                            if(reader["Popularidad"] != DBNull.Value)
                            {
                                veces_vendidas = reader.GetInt32("Popularidad");
                                
                            }
                            else
                            {
                                veces_vendidas = 0;
                            }

                            var articulo = await App.SQLiteDb.GetItemAsync(producto);
                            if( articulo == null)
                            {
                                articulo = new Modelos.Articulo()
                                {
                                    Id = id,
                                    Producto = producto,
                                    Costo = costo,
                                    Precio = precio,
                                    Cantidad = cantidad,
                                    MasterCategory = categoria_general,
                                    Category1 = subcat1,
                                    Category2 = subcat2,
                                    Category3 = subcat3,
                                    VecesVendidas = veces_vendidas
                                };

                                await App.SQLiteDb.SaveItemAsync(articulo);

                            }
                            else
                            {
                                articulo.Producto = (string.Equals("0", producto + "0")) ? articulo.Producto : producto;
                                articulo.Cantidad = (string.Equals("0", cantidad + "0")) ? articulo.Cantidad : cantidad;
                                articulo.Category1 = (string.Equals("0", subcat1 + "0")) ? articulo.Category1 : subcat1;
                                articulo.Category2 = (string.Equals("0", subcat2 + "0")) ? articulo.Category2 : subcat2;
                                articulo.Category3 = (string.Equals("0", subcat3 + "0")) ? articulo.Category3 : subcat3;
                                articulo.MasterCategory = (string.Equals("0", categoria_general + "0")) ? articulo.MasterCategory : categoria_general;
                                articulo.Precio = (string.Equals("0", precio + "0")) ? articulo.Precio : precio;
                                articulo.Costo = (string.Equals("0", costo + "0")) ? articulo.Costo : costo;
                                articulo.VecesVendidas = (string.Equals("0", veces_vendidas + "0")) ? articulo.VecesVendidas : veces_vendidas;

                                await App.SQLiteDb.SaveItemAsync(articulo);
                            }                       
                        }
                        reader.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {

                await DisplayAlert("Paso esto: ", Convert.ToString(ex), "test");
            }
            finally
            {
                con.Close();
            }
        }
    }
}