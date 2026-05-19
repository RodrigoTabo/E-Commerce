using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.Ciudades;
using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Domicilios;
using ROP;

namespace E_Commerce.Application.Services
{
    public class DomicilioService(IDomicilioRepository domicilioRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        ICurrentUserService currentUserService,
        ICiudadService ciudadService) : IDomicilioService
    {

        private readonly IDomicilioRepository _domicilioRepository = domicilioRepository;
        private readonly ICiudadService _ciudadService = ciudadService;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<int>> CreateAsync(CreateDomicilioDTO request)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.BadRequest<int>("Debes estar conectado para esta acción");

            if (request.Altura <= 0 || request.IdLocalidad <= 0)
                return Result.BadRequest<int>("Este campo es obligatorio");

            if (string.IsNullOrWhiteSpace(request.Referencia) || string.IsNullOrWhiteSpace(request.Calle) || string.IsNullOrWhiteSpace(request.CodigoPostal))
                return Result.BadRequest<int>("Este campo es obligatorio");

            var validarLocalidad = await _ciudadService.GetLocalidadById(request.IdLocalidad);
            if (!validarLocalidad.Success)
                return Result.Failure<int>(validarLocalidad.Errors);

            var nuevoDomicilio = new Domicilio
            {
                IdApplicationUser = userId,
                Calle = request.Calle,
                Altura = request.Altura,
                IdCiudad = request.IdLocalidad,
                Referencia = request.Referencia,
                CodigoPostal = request.CodigoPostal,
                UpdatedAt = DateTime.UtcNow,
            };

            await _domicilioRepository.AddAsync(nuevoDomicilio);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(nuevoDomicilio.Id);
        }

        public async Task<Result<List<DomicilioDTO>>> GetAllAsync()
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.BadRequest<List<DomicilioDTO>>("Debes estar conectado para esta acción");

            var result = await _domicilioRepository.GetAllAsync(userId);
            if (!result.Any())
                return Result.NotFound<List<DomicilioDTO>>("La lista esta vacia");

            return Result.Success(result);
        }

        public async Task<Result<Unit>> UpdateAsync(UpdateDomicilioDTO request)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.BadRequest<Unit>("Debes estar conectado para esta acción");

            var domicilio = await _domicilioRepository.ValidarDomicilioExistente(request.Id);
            if (domicilio is null)
                return Result.NotFound<Unit>("El domicilio no existe.");

            if (request.Altura <= 0 || request.IdLocalidad <= 0)
                return Result.BadRequest<Unit>("Este campo es obligatorio");

            if (string.IsNullOrWhiteSpace(request.Referencia) || string.IsNullOrWhiteSpace(request.Calle) || string.IsNullOrWhiteSpace(request.CodigoPostal))
                return Result.BadRequest<Unit>("Este campo es obligatorio");

            var validarLocalidad = await _ciudadService.GetLocalidadById(request.IdLocalidad);
            if (!validarLocalidad.Success)
                return Result.Failure<Unit>(validarLocalidad.Errors);

            domicilio.Altura = request.Altura;
            domicilio.Calle = request.Calle;
            domicilio.CodigoPostal = request.CodigoPostal;
            domicilio.Referencia = request.Referencia;
            domicilio.IdCiudad = request.IdLocalidad;

            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<Unit>> DeleteAsync(int idDomicilio)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.BadRequest<Unit>("Debes estar conectado para esta acción");

            var domicilio = await _domicilioRepository.ValidarDomicilioExistente(idDomicilio);
            if (domicilio is null && domicilio.IdApplicationUser != userId)
                return Result.NotFound<Unit>("El domicilio no existe.");

            await _domicilioRepository.Remove(domicilio);
            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();

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
