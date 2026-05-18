using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Pagos
{
    public class PagoDetailUserDTO
    {
        public EstadoPago EstadoPago { get; set; }
        public decimal Total { get; set; }
        public string? ComprobanteUrl { get; set; }

    }
}
