using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Comentario).IsRequired().HasMaxLength(1000);
            b.Property(b => b.Rating).IsRequired();

            b.HasIndex(b => new { b.IdProducto, b.IdApplicationUser }).IsUnique();

            b.HasOne(u => u.ApplicationUser)
                .WithMany(r => r.Reviews)
                .HasForeignKey(u => u.IdApplicationUser)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(p => p.Producto)
                .WithMany(r => r.Reviews)
                .HasForeignKey(p => p.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
