using E_Commerce.Application.Interfaces.Pagos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class PagoRepository(ECommerceDBContext context) : IPagoRepository
    {
        private readonly ECommerceDBContext _context = context;
        public async Task AddPagoAsync(Pago nuevoPago)
            => await _context.Pagos.AddAsync(nuevoPago);
    }
}
