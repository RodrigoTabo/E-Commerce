using E_Commerce.Application.Interfaces.Ciudades;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Shared.DTOs.Ciudades;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class CiudadService(ICiudadRepository ciudadRepository, IUnitOfWorkRepository unitOfWorkRepository) : ICiudadService
    {
        private readonly ICiudadRepository _ciudadRepository = ciudadRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<List<CiudadesDTO>>> GetAllAsync()
        {
            var ciudades = await _ciudadRepository.GetAllAsync();
            if (ciudades is null)
                return Result.NotFound<List<CiudadesDTO>>("No existe ninguna ciudad");

            return Result.Success(ciudades);
        }

        public async Task<Result<int>> GetLocalidadById(int id)
        {
            var localidad = await _ciudadRepository.GetLocalidadById(id);
            if (localidad <= 0)
                return Result.NotFound<int>("La localidad no existe.");

            return Result.Success(localidad);
        }
    }
}
