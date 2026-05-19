using E_Commerce.Shared.DTOs.Productos;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Favoritos
{
    public class FavoritoResponseDTO
    {
        public int Id { get; set; }
        public ProductoResponseDTO Productos { get; set; }
    }
}
