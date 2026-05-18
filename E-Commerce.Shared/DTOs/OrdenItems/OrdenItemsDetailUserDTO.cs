using E_Commerce.Shared.DTOs.ProductoVariantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.OrdenItems
{
    public class OrdenItemsDetailUserDTO
    {
        public string ProductoNombre { get; set; }
        public ProductoVarianteDTO ProductoVariante { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
