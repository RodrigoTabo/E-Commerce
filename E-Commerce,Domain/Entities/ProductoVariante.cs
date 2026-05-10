using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class ProductoVariante
    {
        public int Id { get; set; }
        public Producto Producto { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public List<ProductoAtributoVariante> ProductoAtributoVariantes { get; set; }
        public List<CarritoItem> CarritoItems { get; set; } = new();
    }
}
