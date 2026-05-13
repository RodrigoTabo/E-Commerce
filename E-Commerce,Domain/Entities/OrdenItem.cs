using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities
{
    public class OrdenItem : IBaseEntity
    {
        public int Id { get; set; }
        public Orden Orden { get; set; } = null!;
        public int IdOrden { get; set; }
        public ProductoVariante ProductoVariante { get; set; } = null!;
        public int IdProductoVariante { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
