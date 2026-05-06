using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Domicilios
{
    public record DomicilioDTO(int Id, string Calle, int Altura, string Ciudad, string CodigoPostal, string? Referencia);
}
