using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.MetodoPagos
{
    public interface IMetodoPagoRepository
    {
        Task<MetodoPago?> ValidarMetodoPagoById(int idMetodoPago);
    }
}
