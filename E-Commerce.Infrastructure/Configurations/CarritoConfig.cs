using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class CarritoConfig : IEntityTypeConfiguration<Carrito>
    {
        public void Configure(EntityTypeBuilder<Carrito> b)
        {
            b.HasKey(b => b.Id);

            b.HasOne(u => u.ApplicationUser)
                .WithOne(c => c.Carrito)
                .HasForeignKey<Carrito>(u => u.IdApplicationUser)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
