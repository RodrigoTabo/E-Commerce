using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.Pagos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Ordenes
{
    public interface IOrdenService
    {
        Task<Result<int>> CreateAsync(CreateOrdenRequest request, PagoRequestDTO pagorequest);
    }
}
