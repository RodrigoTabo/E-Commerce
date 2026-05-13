using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities
{
    public class Producto : IBaseEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Guid IdApplicationUser { get; set; }
        public Modelo Modelo { get; set; } = null!;
        public int? IdModelo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<Review> Reviews { get; set; } = new();
        public List<Favorito> Favoritos { get; set; } = new();
        public List<ProductoVariante> ProductoVariantes { get; set; } = new();
    }
}
