using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Favoritos;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Favoritos
{
    public interface IFavoritoRepository
    {
        Task<List<FavoritoResponseDTO>> GetFavoritosByUser(Guid? userId);
        Task<Favorito?> GetFavoritosAsync(int idProducto, Guid? UserId);
        Task Remove(Favorito favorito);
        Task AddAsync(Favorito favorito);
    }
}
