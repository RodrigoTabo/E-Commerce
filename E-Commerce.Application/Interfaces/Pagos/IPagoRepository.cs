using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Pagos
{
    public interface IPagoRepository
    {
        Task AddPagoAsync(Pago nuevoPago);
    }
}
