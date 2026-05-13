using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.ProductoVariantes;
using ROP;

namespace E_Commerce.Application.Interfaces.ProductoVariantes
{
    public interface IProductoVarianteService
    {
        Task<Result<ProductoVariante>> GetProductoVarianteById(int Id);
        Task<Result<int>> GetStockById(int Id);
        Task<Result<List<ProductoVariante>>> ListaProductosVariantesByIds(List<int> IdsProductosVariantes);
        Task<Result<List<ListProductoVariante>>> GetProductoVarianteByIdProducto(int IdProducto);
        Task<Result<int>> CreateAsync(CreateProductoVarianteDTO request);
        Task<Result<Unit>> UpdateAsync(UpdateProductoVarianteDTO request);
        Task<Result<ProductoVarianteDTO?>> GetByIdAsync(int Id);
    }
}
