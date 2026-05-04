using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Orden
{
    public record CreateOrdenRequest(int? IdDomicilio,int IdMetodoEnvio, decimal Total, string CalleSnapshot, string AlturaSnapshot, string CiudadSnapshot, string CodigoPostaSnapshot);
    
}
