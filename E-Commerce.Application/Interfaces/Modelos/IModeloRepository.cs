using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Modelos
{
    public interface IModeloRepository
    {
        Task<Modelo?> GetByIdAsync(int id);
    }
}
