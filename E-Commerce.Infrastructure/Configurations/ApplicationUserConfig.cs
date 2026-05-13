using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> b)
        {
            // No configuro y no agrego key porque se relaciona con IdentityCore.
            b.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
            b.Property(u => u.Apellido).HasMaxLength(100).IsRequired();
            b.Property(u => u.DNI).HasMaxLength(20).IsRequired();

        }
    }
}
