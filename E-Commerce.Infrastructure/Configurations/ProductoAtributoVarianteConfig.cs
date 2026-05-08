using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class ProductoAtributoVarianteConfig : IEntityTypeConfiguration<ProductoAtributoVariante>
    {
        public void Configure(EntityTypeBuilder<ProductoAtributoVariante> b)
        {
            b.HasKey(x => new { x.IdProductoVariante, x.IdAtributoValor });

            b.HasOne(b => b.ProductoVariante)
                .WithMany(b => b.ProductoAtributoVariantes)
                .HasForeignKey(b => b.IdProductoVariante)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(b => b.AtributoValor)
                .WithMany(b => b.ProductoAtributoVariantes)
                .HasForeignKey(b => b.IdAtributoValor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
