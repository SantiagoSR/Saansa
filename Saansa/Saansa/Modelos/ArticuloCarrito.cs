using System;
using SQLite;

namespace Saansa.Modelos
{
    public class ArticuloCarrito
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }
}