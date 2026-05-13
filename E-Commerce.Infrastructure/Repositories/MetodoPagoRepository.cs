using E_Commerce.Application.Interfaces.MetodoPagos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class MetodoPagoRepository(ECommerceDBContext context) : IMetodoPagoRepository
    {
        private readonly ECommerceDBContext _context = context;
        public async Task<MetodoPago?> ValidarMetodoPagoById(int idMetodoPago)
            => await _context.MetodoPagos.AsNoTracking().Where(mp => mp.Id == idMetodoPago).SingleOrDefaultAsync();
    }
}
