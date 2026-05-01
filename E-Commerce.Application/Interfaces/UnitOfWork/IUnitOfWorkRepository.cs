using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.IUnitOfWorkRepository
{
    public interface IUnitOfWorkRepository
    {
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
