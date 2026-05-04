using E_Commerce.Application.Interfaces.MetodoEnvios;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class MetodoEnvioService(IMetodoEnvioRepository metodoEnvioRepository : IMetodoEnvioService
    {

        private readonly IMetodoEnvioRepository _metodoEnvioRepository = metodoEnvioRepository;

        public async Task<Result<Unit>> MetodoEnvioExistente(int IdMetodoEnvio)
        {
            var metodoExistenteId = await _metodoEnvioRepository.MetodoEnvioExistente(IdMetodoEnvio);

            if (metodoExistenteId <= 0)
               return Result.NotFound<Unit>("El metodo no existe.");

            return Result.Success();
        }
    }
}
