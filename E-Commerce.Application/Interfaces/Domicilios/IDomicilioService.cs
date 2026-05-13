using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Domicilios;
using ROP;

namespace E_Commerce.Application.Interfaces.Domicilios
{
    public interface IDomicilioService
    {
        Task<Result<Domicilio>> ValidarDomicilioExistente(int? IdDomicilio);
        Task<Result<List<DomicilioDTO>>> GetAllAsync();
    }
}
