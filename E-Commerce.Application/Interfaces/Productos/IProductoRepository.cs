using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Productos;


namespace E_Commerce.Application.Interfaces.Productos
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllAsync();
        Task<ProductoDetalleDTO?> GetProductoDetalleAsync(int id);
        Task<Producto?> GetProductoByIdAsync(int id);
        Task AddAsync(Producto producto);
        Task<List<Producto>> ListaProductosByIds(List<int> IdsProductos);
    }
}
