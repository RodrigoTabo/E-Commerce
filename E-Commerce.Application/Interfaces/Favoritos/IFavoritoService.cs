using E_Commerce.Shared.DTOs.Favoritos;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Favoritos
{
    public interface IFavoritoService
    {
        Task<Result<List<FavoritoResponseDTO>>> GetFavoritosByUser();
        Task<Result<Unit>> ToggleFavorito(int IdProducto);
    }
}
