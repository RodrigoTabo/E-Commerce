using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities
{
    public class Carrito : IBaseEntity
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Guid? IdApplicationUser { get; set; }
        public List<CarritoItem> CarritoItems { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
