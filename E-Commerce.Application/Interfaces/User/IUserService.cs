using E_Commerce.Shared.DTOs.User;
using ROP;

namespace E_Commerce.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<Result<UserRequestDTO>> GetByUserId();
        Task<Result<Unit>> ActualizarPerfil(UserRequestDTO request);
    }
}
