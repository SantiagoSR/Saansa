using System;
using SQLite;
namespace Saansa.Modelos
{
    public class ArticuloCarrito
    {
        [PrimaryKey, AutoIncrement]
        public int Cantidad { get; set; }
        public string Producto { get; set; }
        public int Id { get; set; }
        public int Precio { get; set; }
    }
}