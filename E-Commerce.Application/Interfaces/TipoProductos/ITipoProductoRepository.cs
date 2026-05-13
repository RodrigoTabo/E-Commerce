using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.TipoProducto;

namespace E_Commerce.Application.Interfaces.TipoProductos
{
    public interface ITipoProductoRepository
    {
        Task AddAsync(TipoProducto tipoProducto);
        Task<List<TipoProductoDTO>> GetAllAsync();
        Task<int> GetIdByNombreAsync(string nombre);
        Task<TipoProducto?> GetByIdAsync(int id);
    }
}
