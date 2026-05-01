using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class CiudadConfig : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).IsRequired().HasMaxLength(100);
            b.Property(b => b.IdProvincia).IsRequired();

            b.HasOne(p => p.Provincia)
                .WithMany(c => c.Ciudades)
                .HasForeignKey(p => p.IdProvincia)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasData(
                new Ciudad { Id = 1, Nombre = "Mercedes", IdProvincia = 1 },
                new Ciudad { Id = 2, Nombre = "Luján", IdProvincia = 1 },
                new Ciudad { Id = 3, Nombre = "Chivilcoy", IdProvincia = 1 },
            
                new Ciudad { Id = 4, Nombre = "Montevideo Centro", IdProvincia = 3 },
                new Ciudad { Id = 5, Nombre = "Pocitos", IdProvincia = 3 },
                new Ciudad { Id = 6, Nombre = "Malvín", IdProvincia = 3 },
            
                new Ciudad { Id = 7, Nombre = "San Pablo Capital", IdProvincia = 5 },
                new Ciudad { Id = 8, Nombre = "Campinas", IdProvincia = 5 },
                new Ciudad { Id = 9, Nombre = "Santos", IdProvincia = 5 }
            );
        }
    }
}
