using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Ordenes;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class OrdenService(IOrdenRepository ordenRepository, IUnitOfWorkRepository unitOfWorkRepository) : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository = ordenRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
    }
}
