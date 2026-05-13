using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.ProductoVariantes
{
    public class AtributoVarianteDTO
    {
        public int IdAtributo { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdValor { get; set; }
        public string Valor { get; set; } = null!;
    }
}
