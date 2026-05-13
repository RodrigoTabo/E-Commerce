using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Tokens
{
    public interface IGenerarTokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
