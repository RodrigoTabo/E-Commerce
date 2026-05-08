using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> b)
        {
            b.HasKey(b => b.Id);

            b.HasIndex(p => p.Nombre);
            b.HasIndex(p => p.IdModelo);

            b.Property(b => b.Nombre).IsRequired().HasMaxLength(100);
            b.Property(b => b.UrlImagen).IsRequired().HasMaxLength(500);
            b.Property(b => b.Descripcion).IsRequired().HasMaxLength(2000);

            b.HasOne(u => u.ApplicationUser)
                .WithMany(p => p.Productos)
                .HasForeignKey(u => u.IdApplicationUser)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(u => u.Modelo)
                .WithMany(p => p.Productos)
                .HasForeignKey(u => u.IdModelo)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
