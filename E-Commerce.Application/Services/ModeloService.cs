using E_Commerce.Application.Interfaces.Modelos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Modelos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class ModeloService(IModeloRepository modeloRepository) : IModeloService
    {
        private readonly IModeloRepository _modeloRepository = modeloRepository;

        public async Task<Result<ModeloResponseDTO>> GetByIdAsync(int? id)
        {
            var modelo = await _modeloRepository.GetByIdAsync(id);

            var validation = validateModelos(modelo);
            if (!validation.Success)
                return Result.Failure<ModeloResponseDTO>(validation.Errors);
            

            var dto = MapToResponseDTO(validation.Value!);

            return Result.Success(dto);
        }

        public async Task<Result<Modelo>> GetModeloByIdAsync(int? id)
        {
            var modelo = await _modeloRepository.GetByIdAsync(id);
            if(modelo is null)
                return Result.NotFound<Modelo>("El Modelo no existe");

            return Result.Success(modelo);
        }

        //METODOS PRIVADOS

        private Result<Modelo?> validateModelos(Modelo? modelo)
        {
            if (modelo is null)
                return Result.NotFound<Modelo?>("El Modelo no existe");

            return Result.Success(modelo);
        }

        private ModeloResponseDTO MapToResponseDTO(Modelo m)
        {
            return new ModeloResponseDTO
                (
                m.Id,
                m.Nombre,
                m.Marca?.Nombre ?? "Sin Marca",
                m.TipoProducto?.Nombre ?? "Sin Tipo"
                );
        }

    }

}

