using E_Commerce.Application.Interfaces.Reviews;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ReviewRepository(ECommerceDBContext context) : IReviewRepository
    {
        private readonly ECommerceDBContext _context = context;
        public async Task AddReview(Review newReview)
            => await _context.Reviews.AddAsync(newReview);
    }
}
