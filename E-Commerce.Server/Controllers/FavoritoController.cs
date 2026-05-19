using E_Commerce.Application.Interfaces.Favoritos;
using E_Commerce.Shared.DTOs.Favoritos;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController(IFavoritoService favoritoService) : ControllerBase
    {

        private readonly IFavoritoService _favoritoService = favoritoService;

        [HttpGet]
        [ProducesResponseType(typeof(List<FavoritoResponseDTO>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFavoritosByUser()
        {
            var result = await _favoritoService.GetFavoritosByUser();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("{idProducto:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ToggleFavorito([FromRoute] int idProducto)
        {
            var result = await _favoritoService.ToggleFavorito(idProducto);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Success);
        }
    }
}
