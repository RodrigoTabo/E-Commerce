using E_Commerce.Application.Interfaces.MetodoEnvios;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.MetodoEnvios;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class MetodoEnvioRepository(ECommerceDBContext context) : IMetodoEnvioRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task<List<MetodoEnvioDTO>> GetAllAsync()
            => await _context.MetodoEnvios
            .AsNoTracking()
            .Select(m => new MetodoEnvioDTO
            {
                Id = m.Id,
                Nombre = m.Nombre
            })
            .ToListAsync();

        public async Task<int> MetodoEnvioExistente(int IdMetodoEnvio)
            => await _context.MetodoEnvios
            .AsNoTracking()
            .Where(m => m.Id == IdMetodoEnvio)
            .Select(m => m.Id)
            .SingleOrDefaultAsync();
    }
}
