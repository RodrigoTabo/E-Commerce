using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Application.Interfaces.Reviews;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Reviews;
using ROP;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce.Application.Services
{
    public class ReviewService(IReviewRepository reviewRepository,
        IUnitOfWorkRepository unitOfWorkRepository, ICurrentUserService currentUserService,
        IProductoService productoService) : IReviewService
    {

        private IReviewRepository _reviewRepository = reviewRepository;
        private IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private ICurrentUserService _currentUserService = currentUserService;
        private IProductoService _productoService = productoService;

        public async Task<Result<int>> CrearteAsync(CrearReviewDTO request)
        {
            //Validar usuario logueado, validar si el producto existe, validamos que el rating seleccionado sea entre 1 a 5.
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.BadRequest<int>("Debes estar conectado para esta acción.");

            var productoExistente = await ValidarProducto(request.IdProducto);
            if (!productoExistente.Success)
                return Result.NotFound<int>(productoExistente.Errors);

            if (!(request.Rating >= 1 && request.Rating <= 5))
                return Result.BadRequest<int>("Puntuación invalida.");

            if (string.IsNullOrWhiteSpace(request.Comentario))
                return Result.BadRequest<int>("Debes asignar un comentario.");

            var newReview = new Review
            {
                IdApplicationUser = userId,
                Comentario = request.Comentario,
                IdProducto = request.IdProducto,
                Rating = request.Rating
            };

            await _reviewRepository.AddReview(newReview);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(newReview.Id);
        }

        public async Task<Result<Unit>> DeleteAsync(DeleteReviewDTO request)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.Conflict<Unit>("No tienes permitido esta acción");

            var review = await _reviewRepository.GetReview(request.IdReview, userId, request.IdProducto);

            if (!(review is null) && review.IdApplicationUser == userId)
            {
                await _reviewRepository.Remove(review);
                await _unitOfWorkRepository.SaveChangesAsync();
            }
            else
            {
                return Result.NotFound<Unit>("No existe el review.");
            }


            return Result.Success();
        }

        private async Task<Result<Unit>> ValidarProducto(int IdProducto)
        {
            //var productoExistente = await _productoService.GetByIdAsync(IdProducto);

            //if (productoExistente.Value is null)
            //    return Result.NotFound<Unit>("El producto no existe.");

            return Result.Success();
        }


    }
}
