using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.User
{
    public interface IUserRepository
    {
        Task ActualizarPerfil(ApplicationUser actualizarPerfil, Guid? userId);
        Task<ApplicationUser?> GetByUserId(Guid? userId);
    }
}
