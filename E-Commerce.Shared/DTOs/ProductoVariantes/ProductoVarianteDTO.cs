using E_Commerce.Shared.DTOs.AtributoValores;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.ProductoVariantes
{
    public class ProductoVarianteDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public List<AtributoValorDTO> Atributos { get; set; } = [];
    }
}
