using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ProductoVarianteRepository(ECommerceDBContext context) : IProductoVarianteRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task<ProductoVariante?> GetProductoVarianteById(int Id)
            => await _context.ProductoVariantes
            .AsNoTracking()
            .Where(pv => pv.Id == Id)
            .SingleOrDefaultAsync();

        public async Task<int> GetStockById(int Id)
            => await _context.ProductoVariantes
            .AsNoTracking()
            .Where(pv => pv.Id == Id)
            .Select(pv => pv.Stock)
            .SingleOrDefaultAsync();
    }
}
