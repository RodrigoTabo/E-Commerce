using E_Commerce.Shared.DTOs.Atributos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Atributos
{
    public interface IAtributoService
    {
        Task<Result<List<AtributosResponseDTO>>> GetAllAsync();
        Task<Result<int>> CreateAsync(CreateAtributoRequestDTO request);
        Task<Result<Unit>> UpdateAsync(UpdateAtributoRequestDTO request);
        Task<Result<int>> GetByIdAsync(int id); 
    }
}
