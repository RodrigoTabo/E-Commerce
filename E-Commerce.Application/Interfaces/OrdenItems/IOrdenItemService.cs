using E_Commerce.Domain.Entities;
using ROP;

namespace E_Commerce.Application.Interfaces.OrdenItems
{
    public interface IOrdenItemService
    {
        Result<List<OrdenItem>> CrearOrdenItem(Orden orden, List<ProductoVariante> productos, List<CarritoItem> carritoItems);
    }
}
