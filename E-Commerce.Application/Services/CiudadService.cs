using E_Commerce.Application.Interfaces.Ciudades;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
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

        public async Task<Result<int>> GetLocalidadById(int id)
        {
            var localidad = await _ciudadRepository.GetLocalidadById(id);
            if (localidad <= 0)
                return Result.NotFound<int>("La localidad no existe.");

            return Result.Success(localidad);
        }
    }
}
