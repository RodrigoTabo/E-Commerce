using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.MetodoPagos
{
    public interface IMetodoPagoRepository
    {
        Task<MetodoPago?> ValidarMetodoPagoById(int idMetodoPago);
    }
}
