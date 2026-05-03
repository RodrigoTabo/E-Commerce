using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.CarritoItems
{
    public interface ICarritoItemsRepository
    {
        Task<CarritoItem?> ValidarProductoExistente(int productoId, int carritoId);
        Task AgregarProducto(CarritoItem carrito);
        Task<Result<CarritoItem?>> ObtenerItemCarrito(int IdProducto, Guid? userId);
        Task RemoverProducto(CarritoItem request);
    }
}
