using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class DomicilioRepository(ECommerceDBContext context) : IDomicilioRepository
    {
        private readonly ECommerceDBContext _context = context;
        public async Task<Domicilio?> ValidarDomicilioExistente(int? IdDomicilio)
            => await _context.Domicilios
            .AsNoTracking()
            .Where(d => d.Id == IdDomicilio)
            .SingleOrDefaultAsync();
    }
}
