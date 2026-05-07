using E_Commerce.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class Review : IBaseEntity
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Guid? IdApplicationUser { get; set; }
        public Producto Producto { get; set; } = null!;
        public int IdProducto { get; set; }
        public string Comentario { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
