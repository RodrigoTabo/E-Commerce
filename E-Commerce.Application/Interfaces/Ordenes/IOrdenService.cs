using E_Commerce.Shared.DTOs.Orden;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Ordenes
{
    public interface IOrdenService
    {
        Task<Result<int>> CreateAsync(CreateOrdenRequest request);
    }
}
