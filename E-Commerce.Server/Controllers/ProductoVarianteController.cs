using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Shared.DTOs.ProductoVariantes;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/productovariante")]
    public class ProductoVarianteController(IProductoVarianteService productoVarianteService) : ControllerBase
    {

        private readonly IProductoVarianteService _productoVarianteService = productoVarianteService;

        [HttpGet("producto/{idProducto:int}")]
        [ProducesResponseType(typeof(List<ListProductoVariante>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetProductoVarianteByIdProducto(int idProducto)
        {
            var result = await _productoVarianteService
                .GetProductoVarianteByIdProducto(idProducto);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(CreateProductoVarianteDTO request)
        {
            var result = await _productoVarianteService.CreateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Created($"api/productovariante/{result.Value}", new { result.Value });
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(UpdateProductoVarianteDTO request)
        {
            var result = await _productoVarianteService.UpdateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoVarianteDTO>> GetByIdAsync([FromRoute] int id)
        {
            var result = await _productoVarianteService.GetByIdAsync(id);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }
    }
}
