using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Pagos
{
    public class PagoDetailDTO
    {
        public int Id { get; set; }
        public int IdOrden { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreado { get; set; }
        public string? ComprobanteUrl { get; set; }
        public DateTime? FechaCargaComprobante { get; set; }
    }
}
