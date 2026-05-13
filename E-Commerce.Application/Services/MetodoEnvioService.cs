using E_Commerce.Application.Interfaces.MetodoEnvios;
using E_Commerce.Shared.DTOs.MetodoEnvios;
using ROP;

namespace E_Commerce.Application.Services
{
    public class MetodoEnvioService(IMetodoEnvioRepository metodoEnvioRepository) : IMetodoEnvioService
    {

        private readonly IMetodoEnvioRepository _metodoEnvioRepository = metodoEnvioRepository;

        public async Task<Result<List<MetodoEnvioDTO>>> GetAllAsync()
        {
            var result = await _metodoEnvioRepository.GetAllAsync();

            if (!result.Any())
                return Result.NotFound<List<MetodoEnvioDTO>>("La lista esta vacia.");

            return Result.Success(result);
        }

        public async Task<Result<Unit>> MetodoEnvioExistente(int IdMetodoEnvio)
        {
            var metodoExistenteId = await _metodoEnvioRepository.MetodoEnvioExistente(IdMetodoEnvio);

            if (metodoExistenteId <= 0)
                return Result.NotFound<Unit>("El metodo no existe.");

            return Result.Success();
        }
    }
}
