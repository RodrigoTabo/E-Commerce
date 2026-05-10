using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.AtributoValores
{
    public class AtributoValorDTO
    {
        public int Id { get; set; }
        public string Atributo { get; set; } = null!;
        public string Valor { get; set; } = null!;
    }
}
