using E_Commerce.Domain.Entities;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.OrdenItems
{
    public interface IOrdenItemService
    {
        Result<List<OrdenItem>> CrearOrdenItem(Orden orden, List<ProductoVariante> productos, List<CarritoItem> carritoItems);
    }
}
