using E_Commerce.Application.Interfaces.Atributos;
using E_Commerce.Shared.DTOs.Atributos;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/atributo")]
    public class AtributoController(IAtributoService atributoService) : ControllerBase
    {
        private readonly IAtributoService _atributoService = atributoService;

        [HttpGet]
        [ProducesResponseType(typeof(List<AtributosResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _atributoService.GetAllAsync();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("crear")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostAsync([FromBody] CreateAtributoRequestDTO request)
        {
            var result = await _atributoService.CreateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return CreatedAtAction(nameof(GetAsync), new { id = result.Value }, result.Value);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateAtributoRequestDTO request)
        {
            var result = await _atributoService.UpdateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }


    }
}
