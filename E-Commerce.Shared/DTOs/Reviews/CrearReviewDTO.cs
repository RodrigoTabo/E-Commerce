using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Reviews
{
    public record CrearReviewDTO
    {
        public int IdProducto { get; set; }
        public string Comentario { get; set; } = null!;
        public int Rating { get; set; }
    }
}
