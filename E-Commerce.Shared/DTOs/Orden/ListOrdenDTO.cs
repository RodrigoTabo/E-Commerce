using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Orden
{
    public class ListOrdenDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string DomicilioCompleto { get; set; }
        public string MetodoEnvio { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime? FechaExpirado { get; set; }
    }
}
