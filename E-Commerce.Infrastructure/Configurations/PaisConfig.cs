using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class PaisConfig : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).HasMaxLength(100).IsRequired();

            b.HasData(
            new Pais { Id = 1, Nombre = "Argentina" },
            new Pais { Id = 2, Nombre = "Uruguay" },
            new Pais { Id = 3, Nombre = "Brasil" });

        }
    }
}
