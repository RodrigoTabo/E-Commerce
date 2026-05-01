using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public Carrito Carrito { get; set; } = null!;
        public int IdCarrito { get; set; }
        public Producto Producto { get; set; } = null!;
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
