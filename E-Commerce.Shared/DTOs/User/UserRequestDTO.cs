using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.User
{
    public class UserRequestDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? UrlImagen { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
    }
}
