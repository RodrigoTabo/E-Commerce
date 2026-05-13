using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.ProductoVariantes;

namespace E_Commerce.Application.Interfaces.ProductoVariantes
{
    public interface IProductoVarianteRepository
    {
        Task<ProductoVariante?> GetProductoVarianteById(int Id);
        Task AddAsync(ProductoVariante productoVariante);
        Task<int> GetStockById(int Id);
        Task<List<ProductoVariante>> ListaProductosVariantesByIds(List<int> IdsProductosVariantes);
        Task<List<ListProductoVariante>> GetProductoVarianteByIdProducto(int IdProducto);
        Task<int> GetIdProductoVarianteByCombinaciones(int IdProducto, List<int> atributoValorIds);
        Task<ProductoVarianteDTO?> GetByIdAsync(int Id);
        Task RemoveByVarianteIdAsync(int idVariante);
    }
}
