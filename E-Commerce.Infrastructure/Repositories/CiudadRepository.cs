using E_Commerce.Application.Interfaces.Ciudades;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CiudadRepository(ECommerceDBContext context) : ICiudadRepository
    {

        private readonly ECommerceDBContext _context = context;
        public async Task<int> GetLocalidadById(int id)
            => await _context.Ciudades.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => (int?)c.Id)
            .SingleOrDefaultAsync() ?? 0;
    }
}
