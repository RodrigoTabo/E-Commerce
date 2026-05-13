using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class TipoProductoConfig : IEntityTypeConfiguration<TipoProducto>
    {
        public void Configure(EntityTypeBuilder<TipoProducto> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).IsRequired().HasMaxLength(100);
        }
    }
}
