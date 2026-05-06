using E_Commerce.Application.Interfaces.MetodoEnvios;
using E_Commerce.Shared.DTOs.MetodoEnvios;
using E_Commerce.Shared.DTOs.Productos;
using Microsoft.AspNetCore.Mvc;
using ROP;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/metodoEnvio")]
    public class MetodoEnvioController(IMetodoEnvioService metodoEnvioService) : ControllerBase
    {

        private IMetodoEnvioService _metodoEnvioService = metodoEnvioService;

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductoResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ProductoResponseDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _metodoEnvioService.GetAllAsync();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }
    }
}
