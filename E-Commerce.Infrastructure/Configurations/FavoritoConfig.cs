using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class FavoritoConfig : IEntityTypeConfiguration<Favorito>
    {
        public void Configure(EntityTypeBuilder<Favorito> b)
        {
            b.HasKey(b => b.Id);

            b.HasIndex(b => new { b.IdApplicationUser, b.IdProducto }).IsUnique();

            b.HasOne(b => b.Producto)
                .WithMany(b => b.Favoritos)
                .HasForeignKey(b => b.IdProducto)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(b => b.ApplicationUser)
                .WithMany(b => b.Favoritos)
                .HasForeignKey(b => b.IdApplicationUser)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
