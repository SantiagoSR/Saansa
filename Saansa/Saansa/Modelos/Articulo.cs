using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saansa.Modelos
{
    class Articulo
    {
        [PrimaryKey, AutoIncrement]
        public int Cantidad { get; set; }
        public string Producto { get; set; }
    }
}
