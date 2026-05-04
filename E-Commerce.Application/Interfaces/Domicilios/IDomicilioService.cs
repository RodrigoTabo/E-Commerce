using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Domicilios
{
    public interface IDomicilioService
    {
        Task<Result<Domicilio>> ValidarDomicilioExistente(int? IdDomicilio);
    }
}
