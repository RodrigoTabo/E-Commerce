using Azure.Core;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <IActionResult>Post(CreateDomicilioDTO request)
        {
            var result = await _domicilioService.CreateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Created($"api/domicilio/{result.Value}", new { result.Value });
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateDomicilioDTO request)
        {
            var result = await _domicilioService.UpdateAsync(request);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }

        [HttpDelete("{idDomicilio:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(int idDomicilio)
        {
            var result = await _domicilioService.DeleteAsync(idDomicilio);
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);
            return Ok(result.Value);
        }

    }
}
