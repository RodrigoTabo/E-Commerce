using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class ProductoVarianteConfig : IEntityTypeConfiguration<ProductoVariante>
    {
        public void Configure(EntityTypeBuilder<ProductoVariante> b)
        {
            b.HasKey(b => b.Id);
            b.Property(b => b.Stock).IsRequired();
            b.Property(b => b.Precio).IsRequired().HasPrecision(18, 2);

            b.HasIndex(p => new { p.Precio, p.IdProducto });

            b.HasOne(p => p.Producto)
                .WithMany(pv => pv.ProductoVariantes)
                .HasForeignKey(p => p.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
