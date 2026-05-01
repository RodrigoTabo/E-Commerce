using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class DomicilioConfig : IEntityTypeConfiguration<Domicilio>
    {
        public void Configure(EntityTypeBuilder<Domicilio> b)
        {
            b.HasKey(b => b.Id);

            b.Property(x => x.Calle).IsRequired().HasMaxLength(150);
            b.Property(b => b.Altura).IsRequired();
            b.Property(b => b.CodigoPostal).IsRequired().HasMaxLength(20);
            b.Property(b => b.Referencia).IsRequired().HasMaxLength(250);


            b.HasOne(c => c.Ciudad)
                .WithMany(c => c.Domicilios)
                .HasForeignKey(c => c.IdCiudad)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(u => u.ApplicationUser)
                .WithMany(c => c.Domicilios)
                .HasForeignKey(u => u.IdApplicationUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
