using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<Modelo> Modelos { get; set; } = new();
    }
}
