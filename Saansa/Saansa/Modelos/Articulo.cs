using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Saansa.Modelos
{
    [Table(nameof(Articulo))]
    public class Articulo
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }

        [NotNull, MaxLength(250)]
        public string Name { get; set; }

        [NotNull, Indexed, MaxLength(15)]
        public string Barcode { get; set; }

        public int Quantity { get; set; }

        public bool IsValid()
        {
            return (!String.IsNullOrWhiteSpace(Name));
        }
    }
}

