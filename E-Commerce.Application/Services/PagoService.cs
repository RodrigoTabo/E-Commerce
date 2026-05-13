using E_Commerce.Application.Interfaces.MetodoPagos;
using E_Commerce.Application.Interfaces.Pagos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Pagos;
using E_Commerce.Shared.Enums;
using ROP;

namespace E_Commerce.Application.Services
{
    public class PagoService(IPagoRepository pagoRepository, IMetodoPagoService metodoPagoService) : IPagoService
    {
        private readonly IPagoRepository _pagoRepository = pagoRepository;
        private readonly IMetodoPagoService _metodoPagoService = metodoPagoService;

        public async Task<Result<Unit>> CrearPagoAsync(Orden orden, PagoRequestDTO request)
        {
            var resultValidarMetodoPago = await ValidarMetodoPago(request.IdMetodoPago);

            var nuevoPago = new Pago
            {
                Orden = orden,
                IdMetodoPago = request.IdMetodoPago,
                Monto = request.MontoTotal,
                EstadoPago = EstadoPago.Pendiente,
            };

            await _pagoRepository.AddPagoAsync(nuevoPago);

            return Result.Success();
        }

        public async Task<Result<Pago>> GetPagoByOrdenId(int ordenId)
        {
            var pago = await _pagoRepository.GetPagoByOrdenId(ordenId);
            if (pago is null)
                return Result.NotFound<Pago>("El pago de la orden no existe.");

            return Result.Success(pago);
        }

        private async Task<Result<Unit>> ValidarMetodoPago(int IdMetodoPago)
        {
            var result = await _metodoPagoService.ValidarMetodoPagoById(IdMetodoPago);
            if (!result.Success)
                Result.Failure<Unit>(result.Errors);

            return Result.Success();
        }

    }
}
