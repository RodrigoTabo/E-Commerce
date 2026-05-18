using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace E_Commerce.Shared.DTOs.Productos
{
    public record CreateProductoRequestDTO
    {
        public string? Nombre { get; set; }
        public string? UrlImagen { get; set; }
        [JsonIgnore]
        public IBrowserFile? ImagenPerfil { get; set; }
        public IFormFile? ImagenPerfilForm { get; set; }
        public string? Descripcion { get; set; }
        public int? IdModelo { get; set; }
    }

}
