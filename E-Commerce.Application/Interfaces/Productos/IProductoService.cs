using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Productos;
using ROP;

namespace E_Commerce.Application.Interfaces.Productos
{
    public interface IProductoService
    {
        Task<Result<List<ProductoResponseDTO>>> GetAllAsync();
        Task<Result<ProductoDetalleDTO>> GetProductoDetalleAsync(int id);
        Task<Result<int>> CreateAsync(CreateProductoRequestDTO request);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<List<Producto>>> ListaProductosByIds(List<int> IdsProductos);
        //Result<Unit> DescontarStock(List<Producto> productos, List<CarritoItem> carritoItems);
        Task<Result<Unit>> UpdateAsync(UpdateProductoRequestDTO request);
    }
}
