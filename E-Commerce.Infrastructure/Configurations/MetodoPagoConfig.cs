using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class MetodoPagoConfig : IEntityTypeConfiguration<MetodoPago>
    {
        public void Configure(EntityTypeBuilder<MetodoPago> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).IsRequired().HasMaxLength(50);
        }
    }
}
