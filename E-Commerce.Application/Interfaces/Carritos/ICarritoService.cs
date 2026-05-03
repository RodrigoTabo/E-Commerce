using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Carritos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Carritos
{
    public interface ICarritoService
    {
        Task<Result<int>> AgregarAlCarrito(AgregarCarritoDTO request);
        Task<Result<CarritoDto?>> CarritoUser();
        Task<Result<CarritoDto?>> SumarCantidad(int IdProducto);
        Task<Result<CarritoDto?>> RestarCantidad(int IdProducto);
    }
}
