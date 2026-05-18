using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Orden
{
    public class ComprobanteUploadDTO
    {
        public int OrdenId { get; set; }
        public IFormFile Archivo { get; set; } = default!;
    }
}
