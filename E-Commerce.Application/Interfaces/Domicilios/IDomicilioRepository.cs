using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Domicilios;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Domicilios
{
    public interface IDomicilioRepository
    {
        Task<Domicilio?> ValidarDomicilioExistente(int? IdDomicilio);
        Task<List<DomicilioDTO>> GetAllAsync(Guid? userId);
    }
}
