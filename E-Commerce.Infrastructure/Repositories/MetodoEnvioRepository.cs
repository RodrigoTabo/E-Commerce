using E_Commerce.Application.Interfaces.MetodoEnvios;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class MetodoEnvioRepository(ECommerceDBContext context) : IMetodoEnvioRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task<int> MetodoEnvioExistente(int IdMetodoEnvio)
            => await _context.MetodoEnvios
            .AsNoTracking()
            .Where(m => m.Id == Id)
            .Select(m => m.Id)
            .SingleOrDefaultAsync();
    }
}
