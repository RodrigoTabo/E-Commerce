using E_Commerce.Domain.Entities;
using ROP;

namespace E_Commerce.Application.Interfaces.CarritoItems
{
    public interface ICarritoItemsRepository
    {
        Task<CarritoItem?> ValidarCarritoItemExistente(int idProductoVariante, int carritoId);
        Task AgregarProductoVarianteEnCarritoItem(CarritoItem carrito);
        Task<Result<CarritoItem?>> ObtenerItemCarrito(int IdProducto, Guid? userId);
        Task<List<CarritoItem?>> ObtenerItemCarritoByIdUser(Guid? userId);
        Task RemoverProducto(CarritoItem request);
    }
}
