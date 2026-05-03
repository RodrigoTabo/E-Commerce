using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Carritos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Carritos
{
    public interface ICarritoRepository
    {
        Task AddAsync(Carrito carrito);
        Task<int> ObtenerPorUsuario(Guid? userId);
        Task<Result<CarritoDto?>> CarritoUser(Guid? userId);
    }
}
