using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Datas
{
    //Configuracion base.
    public class ECommerceDBContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options) : base(options) {}
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Carrito> Carritos => Set<Carrito>();
        public DbSet<CarritoItem> CarritoItems => Set<CarritoItem>();
        public DbSet<Ciudad> Ciudades => Set<Ciudad>();
        public DbSet<Domicilio> Domicilios => Set<Domicilio>();
        public DbSet<Favorito> Favoritos => Set<Favorito>();
        public DbSet<Marca> Marcas => Set<Marca>();
        public DbSet<MetodoEnvio> MetodoEnvios => Set<MetodoEnvio>();
        public DbSet<MetodoPago> MetodoPagos => Set<MetodoPago>();
        public DbSet<Modelo> Modelos => Set<Modelo>();
        public DbSet<Orden> Ordenes => Set<Orden>();
        public DbSet<OrdenItem> OrdenItems => Set<OrdenItem>();
        public DbSet<Pago> Pagos => Set<Pago>();
        public DbSet<Pais> Paises => Set<Pais>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Provincia> Provincias => Set<Provincia>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<TipoProducto> TipoProductos => Set<TipoProducto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDBContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<IBaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Entity.UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.CreatedAt = DateTime.Now;
                }
                else
                {
                    entityEntry.Property(x => x.CreatedAt).IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
