using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Configurations
{
    public class AtributoConfig : IEntityTypeConfiguration<Atributo>
    {
        public void Configure(EntityTypeBuilder<Atributo> b)
        {
            b.HasKey(b => b.Id);

            b.Property(b => b.Nombre).IsRequired().HasMaxLength(100);
            b.HasIndex(b => b.Nombre).IsUnique();
        }
    }
}
