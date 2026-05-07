using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.Tokens;
using E_Commerce.Shared.DTOs.Auth;
using E_Commerce.Shared.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService, IGenerarTokenService generarTokenService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IGenerarTokenService _generarTokenService = generarTokenService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _authService.LoginAsync(login.Identifier, login.Password);

            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            var generarToken = await _generarTokenService.GenerateTokenAsync(user);

            return Ok(new { Token = generarToken });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
                return StatusCode((int)result.HttpStatusCode, result.Errors);

            var generarToken = await _generarTokenService.GenerateTokenAsync(result.Value);

            return Ok(new { Token = generarToken });
        }
    }
}
