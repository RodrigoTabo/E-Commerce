using E_Commerce.Application.Interfaces.Marcas;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Marcas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class MarcaRepository(ECommerceDBContext context) : IMarcaRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Marca marca)
            => await _context.Marcas.AddAsync(marca);

        public async Task<List<MarcaDTO>> GetAllAsync()
            => await _context.Marcas
            .AsNoTracking()
            .Select(m => new MarcaDTO { Id = m.Id, Nombre = m.Nombre })
            .ToListAsync();

        public async Task<int> GetIdByNombreAsync(string nombre)
            => await _context.Marcas
            .Where(m => m.Nombre == nombre)
            .Select(m => (int?)m.Id)
            .SingleOrDefaultAsync() ?? 0;

        public async Task<Marca?> GetByIdAsync(int id)
            => await _context.Marcas
            .Where(m => m.Id == id)
            .SingleOrDefaultAsync();
    }
}
