using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Domicilios;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class DomicilioService(IDomicilioRepository domicilioRepository, ICurrentUserService currentUserService) : IDomicilioService
    {

        private readonly IDomicilioRepository _domicilioRepository = domicilioRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<List<DomicilioDTO>>> GetAllAsync()
        {
            var userId = _currentUserService.UserId;
            if(userId is null)
                return Result.BadRequest<List<DomicilioDTO>>("Debes estar conectado para esta acción");

            var result = await _domicilioRepository.GetAllAsync(userId);
            if (!result.Any())
                return Result.NotFound<List<DomicilioDTO>>("La lista esta vacia");

            return Result.Success(result);
        }

        public async Task<Result<Domicilio>> ValidarDomicilioExistente(int? IdDomicilio)
        {
            var domicilioExistenteId = await _domicilioRepository.ValidarDomicilioExistente(IdDomicilio);

            if (domicilioExistenteId is null)
                return Result.NotFound<Domicilio>("El domicilio no existe");

            return Result.Success(domicilioExistenteId);

        }
    }
}
