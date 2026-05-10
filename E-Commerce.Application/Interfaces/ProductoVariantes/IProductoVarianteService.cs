using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.ProductoVariantes
{
    public interface IProductoVarianteService
    {
        Task<Result<ProductoVariante>> GetProductoVarianteById(int Id);
        Task<Result<int>> GetStockById(int Id);
    }
}
