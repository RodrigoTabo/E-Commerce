using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class PagoConfig : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Monto).HasPrecision(18, 2).IsRequired();
            b.Property(b => b.EstadoPago).HasConversion<string>().HasMaxLength(30).IsRequired();
            //b.Property(b => b.IdTransaccion).IsRequired().HasMaxLength(100);

            //b.HasIndex(p => p.IdTransaccion).IsUnique();

            b.HasOne(m => m.MetodoPago)
                .WithMany(p => p.Pagos)
                .HasForeignKey(m => m.IdMetodoPago)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(m => m.Orden)
                .WithMany(p => p.Pagos)
                .HasForeignKey(m => m.IdOrden)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
