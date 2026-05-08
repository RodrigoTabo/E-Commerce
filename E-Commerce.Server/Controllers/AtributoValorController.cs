using E_Commerce.Application.Interfaces.AtributoValores;
using E_Commerce.Shared.DTOs.AtributoValores;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/atributovalor")]
    public class AtributoValorController(IAtributoValorService atributoValorService) : ControllerBase
    {

        private readonly IAtributoValorService _atributoValorService = atributoValorService;

        [HttpGet]
        [ProducesResponseType(typeof(List<ListAtributoValorResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _atributoValorService.GetAllAsync();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("crear")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostAsync([FromBody] CreateAtributoValorRequestDTO request)
        {
            var result = await _atributoValorService.CreateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return CreatedAtAction(nameof(GetAsync), new { id = result.Value }, result.Value);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateAtributoValorRequestDTO request)
        {
            var result = await _atributoValorService.UpdateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }
    }
}
