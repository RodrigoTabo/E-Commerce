using E_Commerce.Shared.DTOs.Reviews;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Reviews
{
    public interface IReviewService
    {
        Task<Result<int>> CrearteAsync(CrearReviewDTO request);
    }
}
