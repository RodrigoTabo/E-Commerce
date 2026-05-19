using E_Commerce.Application.Interfaces.Favoritos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Favoritos;
using E_Commerce.Shared.DTOs.Productos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class FavoritoRepository(ECommerceDBContext context) : IFavoritoRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Favorito favorito)
            => await _context.AddAsync(favorito);

        public async Task<Favorito?> GetFavoritosAsync(int idProducto, Guid? userId)
        => await _context.Favoritos.FirstOrDefaultAsync(f => f.IdProducto == idProducto && f.IdApplicationUser == userId);

        public async Task<List<FavoritoResponseDTO>> GetFavoritosByUser(Guid? userId)
            => await _context.Favoritos.Where(f => f.IdApplicationUser == userId).Select(f => new FavoritoResponseDTO
            {
                Id = f.Id,
                Productos = new ProductoResponseDTO
                {
                    Id = f.Producto.Id,
                    Nombre = f.Producto.Nombre,
                    Descripcion = f.Producto.Descripcion,
                    UrlImagen = f.Producto.UrlImagen ?? "Imagen no cargada.",
                    MarcaNombre = f.Producto.Modelo.Marca.Nombre ?? "Sin Marca",
                    CategoriaNombre = f.Producto.Modelo.TipoProducto.Nombre ?? "Sin Tipo",
                    Modelo = f.Producto.Nombre ?? "Sin Modelo", 
                    IdModelo = f.Producto.IdModelo,
                    NombreVendedor = f.Producto.ApplicationUser.Nombre ?? "Sin Usuario",
                    CreateAt = f.Producto.CreatedAt
                }
            }).ToListAsync();

        public Task Remove(Favorito favorito)
        {
            _context.Remove(favorito);
            return Task.CompletedTask;
        }
    }
}
