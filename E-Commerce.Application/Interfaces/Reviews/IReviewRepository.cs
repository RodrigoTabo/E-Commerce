using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Reviews
{
    public interface IReviewRepository
    {
        Task AddReview(Review newReview);
        Task<Review?> GetReview(int idReview, Guid? idUser, int idProducto);
        Task Remove(Review review);
    }
}
