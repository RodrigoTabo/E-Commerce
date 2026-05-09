using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Modelos
{
    public class CreateModeloDTO
    {
        public string Nombre { get; set; }
        public int IdMarca { get; set; }
        public int IdTipoProducto { get; set; }
    }
}
