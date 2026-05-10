using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Shared.DTOs.Productos;
using Microsoft.AspNetCore.Mvc;
using ROP;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController(IProductoService productoService) : ControllerBase
    {
        private readonly IProductoService _productoService = productoService;

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductoResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productoService.GetAllAsync();

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDetalleDTO>> GetByIdAsync([FromRoute] int id)
        {
            var result = await _productoService.GetProductoDetalleAsync(id);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateProductoRequestDTO request)
        {
            var result = await _productoService.CreateAsync(request);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Created($"api/productos/{result.Value}", new { result.Value });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateProductoRequestDTO request)
        {
            var result = await _productoService.UpdateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }
    }
}
