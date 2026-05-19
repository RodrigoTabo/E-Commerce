using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.Favoritos;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Favoritos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class FavoritoService(IFavoritoRepository favoritoRepository, ICurrentUserService currentUserService, IUnitOfWorkRepository unitOfWorkRepository) : IFavoritoService
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IFavoritoRepository _favoritoRepository = favoritoRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<List<FavoritoResponseDTO>>> GetFavoritosByUser()
        {
            //1. Buscamos user.
            var userId = _currentUserService.UserId;

            if (userId is null)
                return Result.Conflict<List<FavoritoResponseDTO>>("Debes estar conectado para esta acción.");

            var favorito = await _favoritoRepository.GetFavoritosByUser(userId);
            if (favorito is null)
                return Result.NotFound<List<FavoritoResponseDTO>>("No hay lista para mostrar.");

            return Result.Success(favorito);
        }

        public async Task<Result<Unit>> ToggleFavorito(int IdProducto)
        {
            //1. Buscamos user.
            var userId = _currentUserService.UserId;

            if (userId is null)
                return Result.Conflict<Unit>("Debes estar conectado para esta acción.");

            //2.Validar producto existente.

            //3. Buscamos favoritos existente.
            var favorito = await _favoritoRepository.GetFavoritosAsync(IdProducto, userId);

            if (favorito is not null)
            {
                await _favoritoRepository.Remove(favorito);
            }
            else
            {
                var nuevoFavorito = new Favorito
                {
                    IdApplicationUser = userId,
                    IdProducto = IdProducto,
                    CreatedAt = DateTime.UtcNow
                };

                await _favoritoRepository.AddAsync(nuevoFavorito);
            }

            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success();

        }
    }
}
