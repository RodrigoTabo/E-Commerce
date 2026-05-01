using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services
{
    public class AuthService(IAuthRepository authRepository) : IAuthService
    {
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<ApplicationUser?> LoginAsync(string email, string password)
        {
            return await _authRepository.ValidarCredenciales(email, password);
        }

        public Task RegisteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
