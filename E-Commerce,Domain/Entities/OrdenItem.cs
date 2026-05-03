using E_Commerce.Domain.Entities.Common;
using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class OrdenItem : IBaseEntity
    {
        public int Id { get; set; }
        public Orden Orden  { get; set; } = null!;
        public int IdOrden { get; set; }
        public Producto Producto { get; set; } = null!;
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
