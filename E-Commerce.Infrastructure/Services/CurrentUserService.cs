using E_Commerce.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace E_Commerce.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

        public Guid? UserId
        {
            get
            {
                var idClaim = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(idClaim, out var id) ? id : null;
            }
        }

        public string? Email => User?.FindFirstValue(ClaimTypes.Email);

        public bool IsAdmin => User?.IsInRole("Admin") ?? false;
    }
}
