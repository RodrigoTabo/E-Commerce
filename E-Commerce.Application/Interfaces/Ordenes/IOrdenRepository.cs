using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Ordenes
{
    public interface IOrdenRepository
    {
        Task AddAsync(Orden orden);
    }
}
