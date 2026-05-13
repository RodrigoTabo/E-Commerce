using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Orden;
using ROP;

namespace E_Commerce.Application.Interfaces.Ordenes
{
    public interface IOrdenRepository
    {
        Task AddAsync(Orden orden);
        Task<List<ListOrdenDTO>> GetOrdenesAsync();
        Task<OrdenDetailsDTO> GetOrdenById(int id);
        Task<Orden?> GetByIdAsync(int id);
    }
}
