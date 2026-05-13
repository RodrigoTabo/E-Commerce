using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities
{
    public class Domicilio : IBaseEntity
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Guid? IdApplicationUser { get; set; }
        public string Calle { get; set; } = null!;
        public int Altura { get; set; }

        public Ciudad Ciudad { get; set; } = null!;
        public int IdCiudad { get; set; }
        public string CodigoPostal { get; set; } = null!;
        public string? Referencia { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<Orden> Ordenes { get; set; } = null!;
    }
}
