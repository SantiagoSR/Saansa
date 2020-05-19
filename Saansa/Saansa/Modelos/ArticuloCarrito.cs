using System;
using SQLite;

namespace Saansa.Modelos
{
    public class ArticuloCarrito
    {
        /*[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }*/

        [PrimaryKey, AutoIncrement]
        public int Ayudante { get; set; }
        [NotNull, Unique]
        public string Id { get; set; }
        [NotNull, Unique]
        public string Producto { get; set; }
        [NotNull]
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public string MasterCategory { get; set; }
        public int Popularidad { get; set; }
    }
}