using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class UnitOfWorkRepository(ECommerceDBContext context) : IUnitOfWorkRepository
    {
        private IDbContextTransaction? _transaction;
        private ECommerceDBContext _context = context;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
