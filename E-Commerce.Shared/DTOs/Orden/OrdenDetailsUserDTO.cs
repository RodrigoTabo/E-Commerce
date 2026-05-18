using E_Commerce.Shared.DTOs.Domicilios;
using E_Commerce.Shared.DTOs.OrdenItems;
using E_Commerce.Shared.DTOs.Pagos;
using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Orden
{
    public class OrdenDetailsUserDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DomicilioDTO? Domicilio { get; set; }
        public string MetodoEnvio { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime? FechaExpirado { get; set; }
        public List<OrdenItemsDetailUserDTO> OrdenItemsDetailUserDTOs { get; set; }
        public PagoDetailUserDTO PagoDetailUserDTO { get; set; }
    }
}
