using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Carritos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class CarritoItemsService(ICarritoItemsRepository carritoItemsRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        IProductoRepository productoRepository) : ICarritoItemsService
    {
        private ICarritoItemsRepository _carritoItemsRepository = carritoItemsRepository;
        private IProductoRepository _productoRepository = productoRepository;
        private IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<CarritoItem>> AgregarCarritoItems(AgregarCarritoDTO dto, int carritoId)
        {
            var item = await _carritoItemsRepository
                .ValidarProductoExistente(dto.ProductoId, carritoId);

            var producto = await _productoRepository.GetByIdAsync(dto.ProductoId);

            if (producto is null)
                return Result.NotFound<CarritoItem>("El producto no existe");

            int cantidadFinal = item is null ? dto.Cantidad : item.Cantidad + dto.Cantidad;

            if (producto.Stock < cantidadFinal)
                return Result.BadRequest<CarritoItem>("No hay stock suficiente");

            if (item is not null)
            {
                item.Cantidad = cantidadFinal;
            }
            else
            {
                item = new CarritoItem
                {
                    IdCarrito = carritoId,
                    IdProducto = dto.ProductoId,
                    Cantidad = dto.Cantidad,
                    Precio = producto.Precio
                };

                await _carritoItemsRepository.AgregarProducto(item);
            }

            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(item);
        }

        public async Task<Result<CarritoItem?>> ObtenerItemCarrito(int IdProducto, Guid? userId)
        {
            var carritoItem = await _carritoItemsRepository.ObtenerItemCarrito(IdProducto, userId);

            if (!carritoItem.Success)
                return Result.NotFound<CarritoItem?>("El producto no existe");

            return Result.Success(carritoItem.Value);

        }

        public async Task RemoverProducto(CarritoItem request)
            => await _carritoItemsRepository.RemoverProducto(request);
    }
}
