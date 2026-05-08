using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class Atributo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<AtributoValor> AtributoValores { get; set; }
    }
}
