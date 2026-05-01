using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class ModeloConfig : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).IsRequired().HasMaxLength(50);

            b.HasIndex(m => m.Nombre);
            b.HasIndex(m => new { m.IdMarca, m.Nombre, m.IdTipoProducto }).IsUnique();

            b.HasOne(m => m.Marca)
                .WithMany(m => m.Modelos)
                .HasForeignKey(m => m.IdMarca)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(tp => tp.TipoProducto)
                .WithMany(m => m.Modelos)
                .HasForeignKey(tp => tp.IdTipoProducto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
