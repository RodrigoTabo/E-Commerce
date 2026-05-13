using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Pagos
{
    public interface IPagoRepository
    {
        Task AddPagoAsync(Pago nuevoPago);
        Task<Pago?> GetPagoByOrdenId(int ordenId);
    }
}
