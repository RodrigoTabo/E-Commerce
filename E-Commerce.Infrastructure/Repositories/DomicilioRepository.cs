using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Domicilios;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class DomicilioRepository(ECommerceDBContext context) : IDomicilioRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Domicilio domicilio)
            => await _context.Domicilios.AddAsync(domicilio);

        public async Task<List<DomicilioDTO>> GetAllAsync(Guid? userId)
        => await _context.Domicilios.AsNoTracking()
            .Where(d => d.IdApplicationUser == userId)
            .Select(d => new DomicilioDTO(d.Id, d.Calle, d.Altura, d.IdCiudad, d.Ciudad.Nombre, d.CodigoPostal, d.Referencia))
            .ToListAsync();

        public Task Remove(Domicilio domicilio)
        {
            _context.Remove(domicilio);
            return Task.CompletedTask;
        }

        public async Task<Domicilio?> ValidarDomicilioExistente(int? IdDomicilio)
            => await _context.Domicilios
            .Include(c => c.Ciudad)
                .ThenInclude(c => c.Provincia)
                    .ThenInclude(c => c.Pais)
            .Where(d => d.Id == IdDomicilio)
            .SingleOrDefaultAsync();
    }
}
