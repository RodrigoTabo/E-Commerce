using E_Commerce.Application.Interfaces.Modelos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ModeloRepository(ECommerceDBContext context) : IModeloRepository
    {
        private ECommerceDBContext _context = context;

        public async Task<Modelo?> GetByIdAsync(int id)
            => await _context.Modelos
                    .Include(m => m.Marca)
                    .Include(m => m.TipoProducto)
                    .SingleOrDefaultAsync(m => m.Id == id);
    }
}
