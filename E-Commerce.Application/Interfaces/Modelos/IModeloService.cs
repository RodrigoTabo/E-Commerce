using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Modelos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Modelos
{
    public interface IModeloService
    {

        //VALIDACIONES
        Task<Result<ModeloResponseDTO>> GetByIdAsync(int? id);
        Task<Result<List<ModeloDetalleDTO>>> GetAllMoMaProAsync();
        Task<Result<Modelo>> GetModeloByIdAsync(int? id);

        //CRUD
        Task<Result<List<ModeloResponseDTO>>> GetAllAsync();
        Task<Result<int>> CreateAsync(CreateModeloDTO request);
        Task<Result<Unit>> UpdateAsync(ModeloResponseDTO request);
    }
}
