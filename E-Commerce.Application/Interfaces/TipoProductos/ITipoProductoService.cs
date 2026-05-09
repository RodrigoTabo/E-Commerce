using E_Commerce.Shared.DTOs.TipoProducto;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.TipoProductos
{
    public interface ITipoProductoService
    {
        Task<Result<List<TipoProductoDTO>>> GetAllAsync();
        Task<Result<int>> CreateAsync(CreateTipoProductoDTO request);
        Task<Result<Unit>> UpdateAsync(TipoProductoDTO request);
        Task<bool> ValidarByIdAsync(int id);
    }
}