using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Marcas;

namespace E_Commerce.Application.Interfaces.Marcas
{
    public interface IMarcaRepository
    {
        Task AddAsync(Marca marca);
        Task<List<MarcaDTO>> GetAllAsync();
        Task<int> GetIdByNombreAsync(string nombre);
        Task<Marca?> GetByIdAsync(int id);
    }
}
