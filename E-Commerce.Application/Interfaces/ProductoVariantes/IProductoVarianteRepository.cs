using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.ProductoVariantes
{
    public interface IProductoVarianteRepository
    {
        Task<ProductoVariante?> GetProductoVarianteById(int Id);
        Task<int> GetStockById(int Id);
    }
}
