using E_Commerce.Shared.DTOs.Marcas;
using ROP;

namespace E_Commerce.Application.Interfaces.Marcas
{
    public interface IMarcaService
    {
        Task<Result<List<MarcaDTO>>> GetAllAsync();
        Task<Result<int>> CreateAsync(CreateMarcaDTO request);
        Task<Result<Unit>> UpdateAsync(MarcaDTO request);
        Task<bool> ValidarByIdAsync(int id);
    }
}
