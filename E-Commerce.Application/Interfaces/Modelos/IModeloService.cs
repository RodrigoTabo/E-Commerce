using E_Commerce.Shared.DTOs.Modelos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Modelos
{
    public interface IModeloService
    {
        Task<Result<ModeloResponseDTO>> GetByIdAsync(int id);
    }
}
