using E_Commerce.Application.Interfaces.OrdenItems;
using E_Commerce.Domain.Entities;
using ROP;

namespace E_Commerce.Application.Services
{
    public class OrdenItemService : IOrdenItemService
    {

        public Result<List<OrdenItem>> CrearOrdenItem(Orden orden, List<ProductoVariante> productoVariante, List<CarritoItem> carritoItems)
        {
            var productosDict = productoVariante
                .ToDictionary(p => p.Id);

            List<OrdenItem> items = [];

            foreach (var item in carritoItems)
            {
                if (!productosDict.TryGetValue(item.IdProductoVariante, out var productoVariantes))
                    return Result.Failure<List<OrdenItem>>($"Producto {item.IdProductoVariante} no existe.");

                var ordenItem = new OrdenItem
                {
                    Orden = orden,
                    IdProductoVariante = productoVariantes.Id,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = productoVariantes.Precio,
                };

                items.Add(ordenItem);
            }

            return Result.Success(items);

        }
    }
}
