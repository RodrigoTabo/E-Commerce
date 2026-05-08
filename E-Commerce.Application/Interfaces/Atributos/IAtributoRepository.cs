using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Atributos;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Atributos
{
    public interface IAtributoRepository
    {
        Task<List<AtributosResponseDTO>> GetAllAsync();
        Task<int> GetIdByNombreAsync(string nombre);
        Task<Atributo?> GetById(int id);
        Task AddAsync(Atributo atributo);
    }
}
