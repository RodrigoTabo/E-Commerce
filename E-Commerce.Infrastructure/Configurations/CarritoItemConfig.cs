using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class CarritoItemConfig : IEntityTypeConfiguration<CarritoItem>
    {
        public void Configure(EntityTypeBuilder<CarritoItem> b)
        {
            b.HasKey(b => b.Id);
            b.HasIndex(b => new { b.IdCarrito, b.IdProducto }).IsUnique();

            b.Property(b => b.Cantidad).IsRequired();
            b.Property(b => b.Precio).IsRequired().HasPrecision(18, 2);


            b.HasOne(b => b.Producto)
                .WithMany(b => b.CarritoItems)
                .HasForeignKey(b => b.IdProducto)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(b => b.Carrito)
                .WithMany(b => b.CarritoItems)
                .HasForeignKey(b => b.IdCarrito)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
