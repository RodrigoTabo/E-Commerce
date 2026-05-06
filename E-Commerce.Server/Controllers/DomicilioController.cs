using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Shared.DTOs.Domicilios;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/domicilio")]
    public class DomicilioController(IDomicilioService domicilioService) : ControllerBase
    {

        private readonly IDomicilioService _domicilioService = domicilioService;

        [HttpGet]
        [ProducesResponseType(typeof(List<DomicilioDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<DomicilioDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<DomicilioDTO>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _domicilioService.GetAllAsync();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }
    }
}
