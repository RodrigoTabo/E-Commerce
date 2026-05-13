using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class OrdenConfig : IEntityTypeConfiguration<Orden>
    {
        public void Configure(EntityTypeBuilder<Orden> b)
        {
            b.HasKey(b => b.Id);
            b.HasIndex(o => o.CreatedAt);
            b.HasIndex(o => o.IdApplicationUser);

            b.Property(b => b.Total).HasPrecision(18, 2).IsRequired();

            b.Property(o => o.EstadoOrden).HasConversion<string>().HasMaxLength(30).IsRequired();

            b.HasOne(a => a.ApplicationUser)
                .WithMany(o => o.Ordenes)
                .HasForeignKey(a => a.IdApplicationUser)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(d => d.Domicilio)
                .WithMany(o => o.Ordenes)
                .HasForeignKey(d => d.IdDomicilio)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(m => m.MetodoEnvio)
                .WithMany(o => o.Ordenes)
                .HasForeignKey(a => a.IdMetodoEnvio)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
