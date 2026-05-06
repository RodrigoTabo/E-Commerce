using E_Commerce.Shared.DTOs.User;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<Result<UserRequestDTO>> GetByUserId();
        Task<Result<Unit>> ActualizarPerfil(UserRequestDTO request);
    }
}
