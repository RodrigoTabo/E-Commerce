using E_Commerce.Application.Interfaces.Ciudades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController(ICiudadService ciudadService) : ControllerBase
    {
        private readonly ICiudadService _ciudadService = ciudadService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ciudadService.GetAllAsync();
            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Ok(result.Value);
        }
    }
}
