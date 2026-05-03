using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class OrdenItemConfig : IEntityTypeConfiguration<OrdenItem>
    {
        public void Configure(EntityTypeBuilder<OrdenItem> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Cantidad).IsRequired();
            b.Property(b => b.PrecioUnitario).HasPrecision(18, 2).IsRequired();
            //b.Property(o => o.EstadoOrden).HasConversion<string>().HasMaxLength(30).IsRequired();

            b.HasOne(o => o.Orden)
                .WithMany(o => o.OrdenItems)
                .HasForeignKey(o => o.IdOrden)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(p => p.Producto)
                .WithMany(o => o.OrdenItems)
                .HasForeignKey(p => p.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
