using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.MetodoEnvios
{
    public record MetodoEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
