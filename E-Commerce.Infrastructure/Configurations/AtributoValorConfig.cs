using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class AtributoValorConfig : IEntityTypeConfiguration<AtributoValor>
    {
        public void Configure(EntityTypeBuilder<AtributoValor> b)
        {
            b.HasKey(b => b.Id);
            b.Property(b => b.Valor).IsRequired().HasMaxLength(50);

            b.HasIndex(b => new { b.IdAtributo, b.Valor }).IsUnique();

            b.HasOne(a => a.Atributo)
                .WithMany(av => av.AtributoValores)
                .HasForeignKey(a => a.IdAtributo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
