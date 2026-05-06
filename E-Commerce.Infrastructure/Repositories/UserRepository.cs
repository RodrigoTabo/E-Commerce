using E_Commerce.Application.Interfaces.User;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class UserRepository(ECommerceDBContext context) : IUserRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task ActualizarPerfil(ApplicationUser actualizarPerfil, Guid? userId)
            => await _context.ApplicationUsers.AddAsync(actualizarPerfil);

        public async Task<ApplicationUser?> GetByUserId(Guid? userId)
         => await _context.ApplicationUsers.Where(u => u.Id == userId).SingleOrDefaultAsync();
    }
}
