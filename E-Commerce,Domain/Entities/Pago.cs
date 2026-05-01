using E_Commerce.Domain.Entities.Common;
using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class Pago : IBaseEntity
    {
        public int Id { get; set; }
        public Orden Orden { get; set; } = null!;
        public int IdOrden { get; set; }
        public MetodoPago MetodoPago { get; set; } = null!;
        public int IdMetodoPago { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public int IdTransaccion { get; set; }
        public decimal Monto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
