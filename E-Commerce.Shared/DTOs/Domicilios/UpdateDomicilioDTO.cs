using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Domicilios
{
    public class UpdateDomicilioDTO
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
        public int IdLocalidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }
    }
}
