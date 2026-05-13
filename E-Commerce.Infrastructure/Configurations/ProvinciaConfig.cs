using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class ProvinciaConfig : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).HasMaxLength(50).IsRequired();
            b.Property(b => b.IdPais).IsRequired();

            b.HasOne(p => p.Pais)
                .WithMany(p => p.Provincias)
                .HasForeignKey(x => x.IdPais)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasData(
            new Provincia { Id = 1, Nombre = "Buenos Aires", IdPais = 1 },
                new Provincia { Id = 2, Nombre = "Córdoba", IdPais = 1 },
                new Provincia { Id = 3, Nombre = "Montevideo", IdPais = 2 },
                new Provincia { Id = 4, Nombre = "Canelones", IdPais = 2 },
                new Provincia { Id = 5, Nombre = "San Pablo", IdPais = 3 },
                new Provincia { Id = 6, Nombre = "Río de Janeiro", IdPais = 3 });
        }
    }
}
