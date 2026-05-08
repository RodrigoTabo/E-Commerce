using E_Commerce.Shared.DTOs.AtributoValores;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.AtributoValores
{
    public interface IAtributoValorService
    {
        Task<Result<List<ListAtributoValorResponseDTO>>> GetAllAsync();
        Task<Result<int>> CreateAsync(CreateAtributoValorRequestDTO request);
        Task<Result<Unit>> UpdateAsync(UpdateAtributoValorRequestDTO request);
    }
}
