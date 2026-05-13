using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class MarcaConfig : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).IsRequired().HasMaxLength(50);
        }
    }
}
