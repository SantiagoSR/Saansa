using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saansa.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Saansa.Views;
using MySql.Data.MySqlClient;
using System.Data;

namespace Saansa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inventario : ContentPage
    {

        private readonly List<string> CategoryMain = new List<string>
        {
            "Bebidas", "Mecato", "Medicamentos", "Otros", "Reposteria"
        };
        public Inventario()
        {
            InitializeComponent();
            var ayudante = CategoryMain;
            
            
        }
        private MySqlConnection con = new MySqlConnection(PaginaPrincipal.conexion);
        private string id, producto, categoria_general;
        private int cantidad, precio;

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtProducto.Text) && !string.IsNullOrEmpty(txtPrecio.Text) && !string.IsNullOrEmpty(txtCosto.Text))
            {
                Modelos.Articulo articulo = new Modelos.Articulo()
                {
                    Id = txtProducto.Text,
                    Producto = txtNombre.Text,
                    Costo = Convert.ToInt32(txtCosto.Text),
                    Precio = Convert.ToInt32(txtPrecio.Text),
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                    MasterCategory = Convert.ToString(MainPicker.Items[MainPicker.SelectedIndex]),
                    Category1 = Convert.ToString(SubCat1.Items[SubCat1.SelectedIndex]),
                    Category2 = Convert.ToString(SubCat2.Items[SubCat2.SelectedIndex]),
                    Category3 = Convert.ToString(SubCat3.Items[SubCat3.SelectedIndex])
                };
               

                //Add New Person
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                        string sql = "INSERT INTO articulos(Id,Producto,Precio,Costo,Cantidad,Categoria_General,SubCategoria1,SubCategoria2,SubCategoria3) VALUES(@Id,@Producto,@Precio,@Costo,@Cantidad,@Categoria_General,@SubCategoria1,@SubCategoria2,@SubCategoria3)";
                        using (MySqlCommand cmd = new MySqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@Id", txtProducto.Text);
                            cmd.Parameters.AddWithValue("@Producto", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Precio", Convert.ToInt32(txtCosto.Text));
                            cmd.Parameters.AddWithValue("@Costo", Convert.ToInt32(txtCosto.Text));
                            cmd.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(txtCantidad.Text));
                            cmd.Parameters.AddWithValue("@Categoria_General", Convert.ToString(MainPicker.Items[MainPicker.SelectedIndex]));
                            cmd.Parameters.AddWithValue("@SubCategoria1", Convert.ToString(SubCat1.Items[SubCat1.SelectedIndex]));
                            cmd.Parameters.AddWithValue("@SubCategoria2", Convert.ToString(SubCat2.Items[SubCat2.SelectedIndex]));
                            cmd.Parameters.AddWithValue("@SubCategoria3", Convert.ToString(SubCat3.Items[SubCat3.SelectedIndex]));
                            cmd.CommandType = CommandType.Text;
                            int result = cmd.ExecuteNonQuery();
                            if (result < 0)
                            {
                                await DisplayAlert("Paso esto: ", "Error añadiendo Articulo", "test");
                            }


                            await App.SQLiteDb.SaveItemAsync(articulo);

                            txtNombre.Text = string.Empty;
                            txtCantidad.Text = string.Empty;
                            txtProducto.Text = string.Empty;
                            txtCosto.Text = string.Empty;
                            txtPrecio.Text = string.Empty;
                            await DisplayAlert("Success", "Articulo añadido con exito ", "OK");
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
            else
            {
                await DisplayAlert("Required", "Recuerda Ingresar un Codigo, Nombre producto, Precio y Costo de este", "OK");
            }
        }

        

        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                //Get Person
                var articulo = await App.SQLiteDb.GetItemAsync(txtNombre.Text);
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        
                        
                        string sql = "SELECT Id,Producto,Cantidad,Precio,Categoria_General FROM articulos WHERE Producto= '"+txtNombre.Text+ "'";
                        using (MySqlCommand cmd = new MySqlCommand(sql, con))
                        {
                            con.Open();
                            MySqlDataReader reader = cmd.ExecuteReader();
                            int count = 0;
                            while (reader.Read())
                            {
                                id = reader.GetString("Id");
                                producto = reader.GetString("Producto");
                                cantidad = reader.GetInt32("Cantidad");
                                precio = reader.GetInt32("Precio");
                                categoria_general = reader.GetString("Categoria_General");

                                count = count + 1;
                            }
                            if (count == 0)
                            {
                                await DisplayAlert("Alerta","No hay articulos con ese nombre en BD","OK");
                            }else if (count == 1)
                            {
                                await DisplayAlert("EXITO", "Identificacion: " + id + "\nNombre Articulo: " + producto + "\nCantidad: " + cantidad + "\nPrecio: " + precio
                        + "\nCategoria: " + categoria_general ,"OK");
                            }else if (count >=2)
                            {
                                await DisplayAlert("Alerta", "Articulos duplicados", "OK");

                            }
                            
                        }
                    }
                }
                catch(MySqlException ex)
                {
                    
                    if (articulo != null && con == null && con.State == ConnectionState.Closed)
                    {
                        txtNombre.Text = articulo.Producto;
                        await DisplayAlert("Success", "Identificacion: " + articulo.Id + "\nNombre Articulo: " + articulo.Producto + "\nCantidad: " + articulo.Cantidad + "\nPrecio: " + articulo.Precio
                            + "\nCategoria: " + articulo.MasterCategory, "OK");
                    }
                    await DisplayAlert("Paso esto: ", Convert.ToString(ex) + "Fallo en conexion, se mostro en la copia local", "test");
                }
                finally
                {
                    con.Close();
                }
                
            }
            else
            {
                await DisplayAlert("Required", "Ingresa Nombre del Articulo", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                Modelos.Articulo articulo = await App.SQLiteDb.GetItemAsync(txtNombre.Text);

                //Aparentemente dejar el texto vacío no es equivalente a "", por eso el +"0"
                articulo.Cantidad = (string.Equals("0", txtCantidad.Text + "0")) ? articulo.Cantidad : Convert.ToInt32(txtCantidad.Text);
                articulo.Category1 = (string.Equals("0", Convert.ToString(SubCat1.Items[SubCat1.SelectedIndex]) + "0")) ? articulo.Category1 : Convert.ToString(SubCat1.Items[SubCat1.SelectedIndex]);
                articulo.Category2 = (string.Equals("0", Convert.ToString(SubCat2.Items[SubCat2.SelectedIndex]) + "0")) ? articulo.Category2 : Convert.ToString(SubCat1.Items[SubCat1.SelectedIndex]);
                articulo.Category3 = (string.Equals("0", Convert.ToString(SubCat3.Items[SubCat3.SelectedIndex]) + "0")) ? articulo.Category3 : Convert.ToString(SubCat1.Items[SubCat1.SelectedIndex]);
                articulo.MasterCategory = (string.Equals("0", Convert.ToString(MainPicker.Items[MainPicker.SelectedIndex]) + "0")) ? articulo.MasterCategory : Convert.ToString(MainPicker.Items[MainPicker.SelectedIndex]);
                articulo.Precio = (string.Equals("0", txtPrecio.Text + "0")) ? articulo.Precio : Convert.ToInt32(txtPrecio.Text);
                articulo.Costo = (String.Equals("0", txtCosto.Text + "0")) ? articulo.Costo : Convert.ToInt32(txtCosto.Text);
                articulo.Producto = (string.Equals("0", txtNombre.Text + "0")) ? articulo.Producto : txtNombre.Text;
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        string sql = "UPDATE articulos SET Producto='"+Convert.ToString(articulo.Producto)+ "',Precio='"+Convert.ToInt32(articulo.Precio)+ "',Costo='"+Convert.ToInt32(articulo.Costo)+ "',Cantidad='"+Convert.ToInt32(articulo.Cantidad)+ "',Categoria_General='"+Convert.ToString(articulo.MasterCategory)+ "',SubCategoria1='"+Convert.ToString(articulo.Category1)+ "',SubCategoria2='" + Convert.ToString(articulo.Category2) + "',SubCategoria3='" + Convert.ToString(articulo.Category3) + "' WHERE Producto = '" + txtNombre.Text + "'";
                        using (MySqlCommand cmd = new MySqlCommand(sql, con))
                        {
                            con.Open();
                            MySqlDataReader reader = cmd.ExecuteReader();
                            int count = 0;
                            while (reader.Read())
                            {
                                count = count + 1;
                            }
                            if (count == 0)
                            {
                                await App.SQLiteDb.SaveItemAsync(articulo);
                                txtNombre.Text = string.Empty;
                                txtCantidad.Text = string.Empty;
                                txtProducto.Text = string.Empty;
                                txtCosto.Text = string.Empty;
                                txtPrecio.Text = string.Empty;
                                await DisplayAlert("Success", "Producto actualizado con éxito", "OK");
                            }
                            else if (count >= 2)
                            {
                                await DisplayAlert("Alerta", "Articulos duplicados y actualizados", "OK");
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    await DisplayAlert("Paso esto: ", Convert.ToString(ex) + "Fallo en conexion Intentelo mas tarde", "test");
                }
                finally
                {
                    con.Close();
                }


            }
            else
            {
                await DisplayAlert("Required", "Please Enter código articulo", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {   //txtProducto
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {

                var articulo = await App.SQLiteDb.GetItemAsync(txtNombre.Text);

                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        string sql = "DELETE FROM articulos WHERE Producto = '" + txtNombre.Text +"'";
                        using (MySqlCommand cmd = new MySqlCommand(sql, con))
                        {
                            con.Open();
                            MySqlDataReader reader = cmd.ExecuteReader();
                            int count = 0;
                            while (reader.Read())
                            {
                                count = count+ 1;
                            }
                            if (count == 0)
                            {
                                await DisplayAlert("Success", "Articulo Borrado Server", "OK");
                                if (articulo != null)
                                {
                                    //Delete Person
                                      await App.SQLiteDb.DeleteItemAsync(articulo);
                                    txtProducto.Text = string.Empty;
                                }

                            }
                            else if (count >= 2)
                            {
                                await DisplayAlert("Alerta", "Articulos duplicados y borrados", "OK");
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    await DisplayAlert("Paso esto: ", Convert.ToString(ex) + "Fallo en conexion Intentelo mas tarde", "test");
                }
                finally
                {
                    con.Close();
                }

               
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Nombre del  Articulo", "OK");
            }
        }
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Bebidas());
        }

    }
}