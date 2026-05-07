using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infrastructure.Repositories
{
    public class AuthRepository(UserManager<ApplicationUser> userManager) : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<ApplicationUser?> CreateAsync(ApplicationUser newUser, string password)
        {
            var identityResult = await _userManager.CreateAsync(newUser, password);

            if (identityResult.Succeeded)
            {
                return newUser;
            }
            return null;
        }

        public async Task<ApplicationUser?> ValidarCredenciales(string identifier, string password)
        {
            var user = await _userManager.FindByEmailAsync(identifier);

            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, password);

            return isValid ? user : null;
        }

        public async Task<ApplicationUser> ValidarUsuarioExistente(string? email)
            => await _userManager.FindByEmailAsync(email);

    }
}
