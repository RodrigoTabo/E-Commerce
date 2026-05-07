using E_Commerce.Shared.DTOs.Reviews;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace E_Commerce.Shared.DTOs.Productos
{
    public record ProductoResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string UrlImagen { get; set; }
        public string MarcaNombre { get; set; }
        public string CategoriaNombre { get; set; }
        public string Modelo { get; set; }
        public string NombreVendedor { get; set; }
        public DateTime CreateAt { get; set; }
        public List<ReviewDTO> ReviewDTO { get; set; } = new();
        public double PromedioEstrellas { get; set; }
        public int TotalReviews { get; set; }
    };
}
