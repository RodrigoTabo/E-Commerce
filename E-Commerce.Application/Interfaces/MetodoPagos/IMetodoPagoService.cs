using ROP;

namespace E_Commerce.Application.Interfaces.MetodoPagos
{
    public interface IMetodoPagoService
    {
        Task<Result<Unit>> ValidarMetodoPagoById(int idMetodoPago);
    }
}
