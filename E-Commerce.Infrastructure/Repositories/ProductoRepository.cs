using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Productos;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ProductoRepository(ECommerceDBContext context) : IProductoRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Producto producto)
            => await _context.Productos.AddAsync(producto);

        public async Task<List<Producto>> GetAllAsync()
            => await _context.Productos
                .AsNoTracking()
                .Include(p => p.Modelo)
                    .ThenInclude(m => m.Marca)
                .Include(p => p.Modelo)
                    .ThenInclude(m => m.TipoProducto)
                .Include(p => p.ApplicationUser)
                .ToListAsync();

        public async Task<ProductoResponseDTO> GetByIdAsync(int id)
        {
            var producto = await _context.Productos
                .Where(p => p.Id == id)
                .Select(p => new ProductoResponseDTO(
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Stock,
                    p.UrlImagen,
                    p.Modelo != null ? p.Modelo.Marca.Nombre : "",
                    p.Modelo != null ? p.Modelo.TipoProducto.Nombre : "",
                    p.Modelo.Nombre,
                    p.ApplicationUser.Nombre,
                    p.CreatedAt
                ))
                .SingleOrDefaultAsync();

            return producto;
        }

    }
}
