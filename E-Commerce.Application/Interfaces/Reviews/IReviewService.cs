using E_Commerce.Shared.DTOs.Reviews;
using ROP;

namespace E_Commerce.Application.Interfaces.Reviews
{
    public interface IReviewService
    {
        Task<Result<int>> CrearteAsync(CrearReviewDTO request);
    }
}
