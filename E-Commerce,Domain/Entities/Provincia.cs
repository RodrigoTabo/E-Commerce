using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public Pais Pais { get; set; } = null!;
        public int IdPais { get; set; }
        public List<Ciudad> Ciudades { get; set; } = new();
    }
}
