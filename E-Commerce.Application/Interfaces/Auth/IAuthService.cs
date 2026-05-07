using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.User;
using ROP;

namespace E_Commerce.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ApplicationUser?> LoginAsync(string identifier, string password);
        Task<Result<ApplicationUser>> RegisterAsync(UserRegisterDTO request);
    }
}
