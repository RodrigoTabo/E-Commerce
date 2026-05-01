using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public Provincia Provincia { get; set; } = null!;
        public int IdProvincia { get; set; }
        public List<Domicilio> Domicilios { get; set; } = new();
    }
}
