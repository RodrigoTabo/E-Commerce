using E_Commerce.Application.Interfaces.Pagos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class PagoRepository(ECommerceDBContext context) : IPagoRepository
    {
        private readonly ECommerceDBContext _context = context;
        public async Task AddPagoAsync(Pago nuevoPago)
            => await _context.Pagos.AddAsync(nuevoPago);

        public async Task<Pago?> GetPagoByOrdenId(int ordenId)
            => await _context.Pagos.Where(p => p.IdOrden == ordenId).SingleOrDefaultAsync();
    }
}
