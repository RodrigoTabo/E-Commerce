using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.AtributoValores;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.AtributoValores
{
    public interface IAtributoValorRepository
    {
        Task AddAsync(AtributoValor atributo);
        Task<List<ListAtributoValorResponseDTO>> GetAllAsync();
        Task<int> GetIdByValorAsync(string valor);
        Task<AtributoValor?> GetByIdAsync(int id);
    }
}
