using E_Commerce.Shared.DTOs.MetodoEnvios;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.MetodoEnvios
{
    public interface IMetodoEnvioService
    {
        Task<Result<Unit>> MetodoEnvioExistente(int IdMetodoEnvio);
        Task<Result<List<MetodoEnvioDTO>>> GetAllAsync();
    }
}
