using E_Commerce.Application.Interfaces.Ordenes;
using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.OrdenCreate;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/orden")]
    public class OrdenController(IOrdenService ordenService) : ControllerBase
    {

        private readonly IOrdenService _ordenService = ordenService;

        [HttpGet]
        [ProducesResponseType(typeof(List<ListOrdenDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _ordenService.GetOrdenesAsync();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("mis-compras")]
        [ProducesResponseType(typeof(List<OrdenDetailsUserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ListOrdenByUsers()
        {
            var result = await _ordenService.ListOrdenByUsers();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdenDetailsDTO>> GetByIdAsync([FromRoute] int id)
        {
            var result = await _ordenService.GetOrdenById(id);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CrearOrdenAsync([FromBody] CrearOrdenConPagoRequest request)
        {
            var result = await _ordenService.CreateAsync(request.Orden, request.Pago);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Created($"api/orden/{result.Value}", new { result.Value });
        }

        [HttpPost("aprobar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AprobarPagoAsync(int ordenId)
        {
            var result = await _ordenService.AprobarPagoAsync(ordenId);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("preparar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PrepararOrdenAsync(int ordenId)
        {
            var result = await _ordenService.PrepararOrdenAsync(ordenId);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("enviar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EnviarOrdenAsync(int ordenId)
        {
            var result = await _ordenService.EnviarOrdenAsync(ordenId);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("entregar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EntregarOrdenAsync(int ordenId)
        {
            var result = await _ordenService.EntregarOrdenAsync(ordenId);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("cargar-comprobante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CargarComprobantes([FromForm] ComprobanteUploadDTO request)
        {
            var result = await _ordenService.CargarComprobante(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }
    }
}
