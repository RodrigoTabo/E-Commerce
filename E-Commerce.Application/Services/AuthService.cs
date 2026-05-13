using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.User;
using ROP;

namespace E_Commerce.Application.Services
{
    public class AuthService(IAuthRepository authRepository, IUnitOfWorkRepository unitOfWorkRepository) : IAuthService
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<ApplicationUser?> LoginAsync(string email, string password)
        {
            return await _authRepository.ValidarCredenciales(email, password);
        }

        public async Task<Result<ApplicationUser>> RegisterAsync(UserRegisterDTO request)
        {
            var validarByEmail = await _authRepository.ValidarUsuarioExistente(request.Email);
            if (validarByEmail is not null)
                return Result.Conflict<ApplicationUser>("El email ya esta registrado.");

            var newUser = new ApplicationUser
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                UserName = request.Email,
                UrlImagen = request.UrlImagen,
                DNI = request.DNI
            };

            var result = await _authRepository.CreateAsync(newUser, request.Password);
            if (result is null)
                return Result.Failure<ApplicationUser>("Ha ocurrido un error al crear la cuenta.");

            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(result);
        }
    }
}
