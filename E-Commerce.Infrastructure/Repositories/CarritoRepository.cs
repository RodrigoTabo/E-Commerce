using E_Commerce.Application.Interfaces.Carritos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Carritos;
using Microsoft.EntityFrameworkCore;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CarritoRepository(ECommerceDBContext context) : ICarritoRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Carrito carrito)
            => await _context.Carritos.AddAsync(carrito);

        public async Task<Result<CarritoDto?>> CarritoUser(Guid? userId)
            => await context.Carritos
                    .AsNoTracking()
                    .Where(c => c.IdApplicationUser == userId)
                    .Select(c => new CarritoDto
                    {
                        Items = c.CarritoItems.Select(ci => new CarritoItemDto
                        {
                            IdProductoVariante = ci.ProductoVariante.Id,
                            ProductoNombre = ci.ProductoVariante.Producto.Nombre,
                            ProductoStock = ci.ProductoVariante.Stock,
                            ProductoUrlImagen = ci.ProductoVariante.Producto.UrlImagen,
                            ModeloNombre = ci.ProductoVariante.Producto.Modelo.Nombre,
                            CarritoPrecio = ci.Precio,
                            CarritoCantidad = ci.Cantidad
                        }).ToList(),
                        Total = c.CarritoItems.Sum(ci => ci.Precio * ci.Cantidad)
                    })
                    .FirstOrDefaultAsync();

        public async Task<int> ObtenerPorUsuario(Guid? userId)
            => await _context.Carritos
                .Where(u => u.IdApplicationUser == userId)
                .Select(c => c.Id)
                .SingleOrDefaultAsync();

    }
}
