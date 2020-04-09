using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saansa.Modelos
{
    public class Articulo
    {
        [PrimaryKey,AutoIncrement]
        public int Ayudante { get; set; }
        [NotNull,Unique]
        public string Id { get; set; }
        [NotNull]
        public string Producto { get; set; }
        [NotNull]
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public string MasterCategory { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
    }
}
