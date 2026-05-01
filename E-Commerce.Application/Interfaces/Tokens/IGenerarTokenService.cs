using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Tokens
{
    public interface IGenerarTokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
