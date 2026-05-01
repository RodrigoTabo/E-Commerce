using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<ApplicationUser?> ValidarCredenciales(string identifier, string password);
    }
}
