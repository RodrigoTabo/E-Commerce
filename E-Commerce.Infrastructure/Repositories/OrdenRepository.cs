using E_Commerce.Application.Interfaces.Ordenes;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class OrdenRepository(ECommerceDBContext context) : IOrdenRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Orden orden)
            => await _context.Ordenes.AddAsync(orden);
    }
}
