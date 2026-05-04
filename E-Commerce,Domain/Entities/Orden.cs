using E_Commerce.Domain.Entities.Common;
using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class Orden : IBaseEntity
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Guid? IdApplicationUser { get; set; }
        public Domicilio Domicilio { get; set; } = null!;
        public int IdDomicilio { get; set; }
        public string CalleSnapshot { get; set; } = null!;
        public int AlturaSnapshot { get; set; }
        public string CiudadSnapshot { get; set; } = null!;
        public string CodigoPostalSnapshot { get; set; } = null!;
        public MetodoEnvio MetodoEnvio { get; set; } = null!;
        public int IdMetodoEnvio { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }    
        public DateTime? ExpiresAt { get; set; }    
        public List<OrdenItem> OrdenItems { get; set; } = new();
        public List<Pago> Pagos { get; set; } = new();
    }
}
