using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class ProductoVarianteService(IProductoVarianteRepository productoVarianteRepository) : IProductoVarianteService
    {

        private readonly IProductoVarianteRepository _productoVarianteRepository = productoVarianteRepository;

        public async Task<Result<ProductoVariante>> GetProductoVarianteById(int Id)
        {
            var existeProductoVariante = await _productoVarianteRepository.GetProductoVarianteById(Id);
            if (existeProductoVariante is null)
                return Result.NotFound<ProductoVariante>("La variante del producto no existe.");

            return Result.Success(existeProductoVariante);
        }

        public async Task<Result<int>> GetStockById(int Id)
        {
            var stock = await _productoVarianteRepository.GetStockById(Id);
            if (stock <= 0)
                return Result.Conflict<int>("La variante del producto no tiene stock");
            return Result.Success(stock);
        }
    }
}
