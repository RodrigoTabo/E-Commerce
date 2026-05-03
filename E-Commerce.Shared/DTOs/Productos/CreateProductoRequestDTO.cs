using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Productos
{
    public record CreateProductoRequestDTO
    {
        public string? Nombre { get; set; }
        public string? UrlImagen { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public int IdModelo { get; set; }
    }

}
