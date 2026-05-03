using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Carritos
{
    public class CarritoDto
    {
        public List<CarritoItemDto> Items { get; set; }
        public decimal Total { get; set; }
    }
}
