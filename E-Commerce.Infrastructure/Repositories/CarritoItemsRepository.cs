using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CarritoItemsRepository(ECommerceDBContext context) : ICarritoItemsRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AgregarProductoVarianteEnCarritoItem(CarritoItem carrito)
            => await _context.CarritoItems.AddAsync(carrito);

        public async Task<Result<CarritoItem?>> ObtenerItemCarrito(int IdProductoVariante, Guid? userId)
            => await _context.CarritoItems
                .Include(ci => ci.Carrito)
                .SingleOrDefaultAsync(ci =>
                    ci.IdProductoVariante == IdProductoVariante &&
                    ci.Carrito.IdApplicationUser == userId);

        public async Task<List<CarritoItem?>> ObtenerItemCarritoByIdUser(Guid? userId)
            => await _context.CarritoItems.AsNoTracking()
                .Where(ci => ci.Carrito.IdApplicationUser == userId)
                .ToListAsync();

        public async Task RemoverProducto(CarritoItem request)
            => _context.CarritoItems.Remove(request);

        public async Task<CarritoItem?> ValidarCarritoItemExistente(int idProductoVariante, int carritoId)
            => await _context.CarritoItems
            .Where(p => p.IdProductoVariante == idProductoVariante && p.IdCarrito == carritoId)
            .SingleOrDefaultAsync();

    }
}
