using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Pagos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Pagos
{
    public interface IPagoService
    {
        Task<Result<Unit>> CrearPagoAsync(Orden orden, PagoRequestDTO request);
    }
}
