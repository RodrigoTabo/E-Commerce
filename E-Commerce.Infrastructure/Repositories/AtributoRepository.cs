using E_Commerce.Application.Interfaces.Atributos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Atributos;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class AtributoRepository(ECommerceDBContext context) : IAtributoRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Atributo atributo)
            => await _context.AddAsync(atributo);

        public async Task<List<AtributosResponseDTO>> GetAllAsync()
            => await _context.Atributos
            .AsNoTracking()
            .Select(a => new AtributosResponseDTO
            {
                Id = a.Id,
                Nombre = a.Nombre
            }).ToListAsync();

        public async Task<Atributo?> GetById(int id)
            => await _context.Atributos
            .Where(a => a.Id == id)
            .SingleOrDefaultAsync();

        public async Task<int> GetIdByNombreAsync(string nombre)
            => await _context.Atributos
            .AsNoTracking()
            .Where(n => n.Nombre == nombre)
            .Select(a => (int?)a.Id)
            .SingleOrDefaultAsync() ?? 0;
    }
}
