using E_Commerce.Application.Interfaces.MetodoPagos;
using ROP;

namespace E_Commerce.Application.Services
{
    public class MetodoPagoService(IMetodoPagoRepository metodoPagoRepository) : IMetodoPagoService
    {

        private readonly IMetodoPagoRepository _metodoPagoRepository = metodoPagoRepository;

        public async Task<Result<Unit>> ValidarMetodoPagoById(int idMetodoPago)
        {
            var result = await _metodoPagoRepository.ValidarMetodoPagoById(idMetodoPago);

            if (result is null)
                return Result.NotFound<Unit>("No existe el metodo de pago.");

            return Result.Success();
        }
    }
}
