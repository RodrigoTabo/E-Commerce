using E_Commerce.Application.Interfaces.Reviews;
using E_Commerce.Shared.DTOs.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody] CrearReviewDTO request)
        {
            var result = await _reviewService.CrearteAsync(request);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            return Created($"api/review/{result.Value}", new { result.Value });
        }
    }
}
