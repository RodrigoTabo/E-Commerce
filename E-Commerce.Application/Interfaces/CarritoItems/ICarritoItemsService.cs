using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Carritos;
using ROP;

namespace E_Commerce.Application.Interfaces.CarritoItems
{
    public interface ICarritoItemsService
    {
        Task<Result<CarritoItem>> AgregarCarritoItems(AgregarCarritoDTO dto, int carritoId);
        Task<Result<CarritoItem?>> ObtenerItemCarrito(int IdProducto, Guid? userId);
        Task<Result<List<CarritoItem?>>> ObtenerItemCarritoByIdUser(Guid? userId);
        Task RemoverProducto(CarritoItem request);
    }
}
