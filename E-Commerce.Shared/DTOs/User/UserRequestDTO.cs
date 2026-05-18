using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace E_Commerce.Shared.DTOs.User
{
    public class UserRequestDTO
    {
        public string? Email { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? UrlImagen { get; set; } = null!;
        [JsonIgnore]
        public IBrowserFile? ImagenPerfil { get; set; }

        public IFormFile? ImagenPerfilForm { get; set; }
        public string DNI { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
