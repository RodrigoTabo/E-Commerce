using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Domicilios;
using ROP;

namespace E_Commerce.Application.Interfaces.Domicilios
{
    public interface IDomicilioService
    {
        Task<Result<Domicilio>> ValidarDomicilioExistente(int? IdDomicilio);
        Task<Result<List<DomicilioDTO>>> GetAllAsync();
        Task<Result<int>> CreateAsync(CreateDomicilioDTO request);
        Task<Result<Unit>> UpdateAsync(UpdateDomicilioDTO request);
        Task<Result<Unit>> DeleteAsync(int idDomicilio);
    }
}
