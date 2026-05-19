using E_Commerce.Application.Interfaces.Ciudades;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Ciudades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CiudadRepository(ECommerceDBContext context) : ICiudadRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task<List<CiudadesDTO>> GetAllAsync()
            => await _context.Ciudades.Select(c => new CiudadesDTO
            {
                Id = c.Id,
                CiudadCompleta = c.Nombre + ", " + c.Provincia.Nombre + ", " + c.Provincia.Pais.Nombre,
            }).ToListAsync();

        public async Task<int> GetLocalidadById(int id)
            => await _context.Ciudades.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => (int?)c.Id)
            .SingleOrDefaultAsync() ?? 0;
    }
}
