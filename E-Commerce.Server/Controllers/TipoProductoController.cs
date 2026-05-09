using E_Commerce.Application.Interfaces.TipoProductos;
using E_Commerce.Shared.DTOs.TipoProducto;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/tipoproducto")]
    public class TipoProductoController(ITipoProductoService tipoProductoService) : ControllerBase
    {

        private ITipoProductoService _tipoProductoService = tipoProductoService;

        [HttpGet]
        [ProducesResponseType(typeof(List<TipoProductoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _tipoProductoService.GetAllAsync();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CreateTipoProductoDTO request)
        {
            var result = await _tipoProductoService.CreateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return CreatedAtAction(nameof(GetAsync), new { id = result.Value }, result.Value);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] TipoProductoDTO request)
        {
            var result = await _tipoProductoService.UpdateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

    }
}
