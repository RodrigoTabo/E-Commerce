using E_Commerce.Application.Interfaces.TipoProductos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.TipoProducto;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class TipoProductoRepository(ECommerceDBContext context) : ITipoProductoRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(TipoProducto tipoProducto)
            => await _context.TipoProductos.AddAsync(tipoProducto);

        public async Task<List<TipoProductoDTO>> GetAllAsync()
            => await _context.TipoProductos
            .AsNoTracking()
            .Select(tp => new TipoProductoDTO { Id = tp.Id, Nombre = tp.Nombre })
            .ToListAsync();

        public async Task<TipoProducto?> GetByIdAsync(int id)
            => await _context.TipoProductos
            .Where(tp => tp.Id == id)
            .SingleOrDefaultAsync();

        public async Task<int> GetIdByNombreAsync(string nombre)
        => await _context.TipoProductos
            .AsNoTracking()
            .Where(tp => tp.Nombre == nombre)
            .Select(tp => (int?)tp.Id)
            .SingleOrDefaultAsync() ?? 0;
    }
}
