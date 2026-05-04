using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class DomicilioService(IDomicilioRepository domicilioRepository) : IDomicilioService
    {

        private readonly IDomicilioRepository _domicilioRepository = domicilioRepository;
        public async Task<Result<Domicilio>> ValidarDomicilioExistente(int? IdDomicilio)
        {
            var domicilioExistenteId = await _domicilioRepository.ValidarDomicilioExistente(IdDomicilio);

            if (domicilioExistenteId is null)
                return Result.NotFound<Domicilio>("El domicilio no existe");

            return Result.Success(domicilioExistenteId);

        }
    }
}
