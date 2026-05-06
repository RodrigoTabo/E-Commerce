using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.MetodoPagos
{
    public interface IMetodoPagoService
    {
        Task<Result<Unit>> ValidarMetodoPagoById(int idMetodoPago);
    }
}
