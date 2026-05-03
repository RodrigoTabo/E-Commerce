using E_Commerce.Application.Interfaces.Carritos;
using E_Commerce.Shared.DTOs.Carritos;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/Carrito")]
    public class CarritoController(ICarritoService carritoService) : ControllerBase
    {
        private readonly ICarritoService _carritoService = carritoService;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObtenerCarrito()
        {
            var result = await _carritoService.CarritoUser();

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AgregarCarrito(AgregarCarritoDTO request)
        {
            var result = await _carritoService.AgregarAlCarrito(request);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Created($"api/Carrito/{result.Value}", new { result.Value });
        }

        [HttpPatch("restar/{IdProducto}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RestarCantidad(int IdProducto)
        {
            var result = await _carritoService.RestarCantidad(IdProducto);

            if(!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPatch("sumar/{IdProducto}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SumarCantidad(int IdProducto)
        {
            var result = await _carritoService.SumarCantidad(IdProducto);

            if(!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }



    }
}
