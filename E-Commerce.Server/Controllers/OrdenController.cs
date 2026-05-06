using E_Commerce.Application.Interfaces.Ordenes;
using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.OrdenCreate;
using E_Commerce.Shared.DTOs.Pagos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/orden")]
    public class OrdenController(IOrdenService ordenService) : ControllerBase
    {

        private readonly IOrdenService _ordenService = ordenService;

        [HttpPost]
        public async Task<IActionResult> CrearOrdenAsync([FromBody] CrearOrdenConPagoRequest request)
        {
            var result = await _ordenService.CreateAsync(request.Orden, request.Pago);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Created($"api/orden/{result.Value}", new { result.Value });
        }
    }
}
