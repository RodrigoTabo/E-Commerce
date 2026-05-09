using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Marcas;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Marcas;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class MarcaService(IMarcaRepository marcaRepository, IUnitOfWorkRepository unitOfWorkRepository) : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository = marcaRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<int>> CreateAsync(CreateMarcaDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return Result.BadRequest<int>("El nombre es obligatorio.");

            var id = await _marcaRepository.GetIdByNombreAsync(request.Nombre);
            if (id != 0)
                return Result.Conflict<int>("La marca ya existe.");

            var newMarca = new Marca
            {
                Nombre = request.Nombre
            };

            await _marcaRepository.AddAsync(newMarca);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(newMarca.Id);
        }

        public async Task<Result<List<MarcaDTO>>> GetAllAsync()
        {
            var lista = await _marcaRepository.GetAllAsync();

            if (!lista.Any())
                return Result.Conflict<List<MarcaDTO>>("La lista esta vacia.");

            return Result.Success(lista);
        }

        public async Task<Result<Unit>> UpdateAsync(MarcaDTO request)
        {
            //1. Validamos que el nombre este cargado.
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return Result.BadRequest<Unit>("El nombre de la marca es obligatorio.");

            //2. Validamos existencia del objeto.
            var marca = await _marcaRepository.GetByIdAsync(request.Id);

            if (marca is null)
                return Result.NotFound<Unit>("La marca no existe.");

            //3. Validamos que el nombre no exista.
            var id = await _marcaRepository.GetIdByNombreAsync(request.Nombre);

            if (id != 0 && id != request.Id)
                return Result.Conflict<Unit>("La marca ya existe.");

            //Modificamos el nombre y guardamos
            marca.Nombre = request.Nombre;

            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<bool> ValidarByIdAsync(int id)
        {
            var marca = await _marcaRepository.GetByIdAsync(id);

            if (marca is null)
                return false;
            
            return true;
        }
    }
}
