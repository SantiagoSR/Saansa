using System;
using SQLite;

namespace Saansa.Modelos
{
    public class ArticuloCarrito
    {
        public string Id { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }
}