using E_Commerce.Application.Interfaces.OrdenItems;
using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class OrdenItemService : IOrdenItemService
    {

        public Result<List<OrdenItem>> CrearOrdenItem(Orden orden, List<Producto> productos, List<CarritoItem> carritoItems)
        {
            var productosDict = productos
                .ToDictionary(p => p.Id);

            List<OrdenItem> items = [];

            foreach (var item in carritoItems)
            {
                if (!productosDict.TryGetValue(item.IdProductoVariante, out var producto))
                    return Result.Failure<List<OrdenItem>>($"Producto {item.IdProductoVariante} no existe.");

                var ordenItem = new OrdenItem
                {
                    Orden = orden,
                    IdProducto = producto.Id,
                    Cantidad = item.Cantidad,
                    //PrecioUnitario = producto.Precio,
                };

                items.Add(ordenItem);
            }

            return Result.Success(items);

        }
    }
}
