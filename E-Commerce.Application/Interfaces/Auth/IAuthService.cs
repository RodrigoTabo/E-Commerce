using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ApplicationUser?> LoginAsync(string identifier, string password);
        Task RegisteAsync();
    }
}
