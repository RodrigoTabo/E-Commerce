using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public List<Domicilio> Domicilios { get; set; } = new();
        public List<Orden> Ordenes { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public Carrito Carrito { get; set; } = null!;
        public int IdCarrito { get; set; }
        public List<Favorito> Favoritos { get; set; } = new();
        public List<Producto> Productos { get; set; } = new();
    }
}
