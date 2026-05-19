using E_Commerce.Application.Interfaces.Reviews;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ReviewRepository(ECommerceDBContext context) : IReviewRepository
    {
        private readonly ECommerceDBContext _context = context;
        public async Task AddReview(Review newReview)
            => await _context.Reviews.AddAsync(newReview);

        public async Task<Review?> GetReview(int idReview, Guid? idUser, int idProducto)
            => await _context.Reviews
            .Where(r => r.IdApplicationUser == idUser && r.IdProducto == idProducto && r.Id == idReview)
            .FirstOrDefaultAsync();

        public Task Remove(Review review)
        {
            _context.Remove(review);
            return Task.CompletedTask;
        }
    }
}
