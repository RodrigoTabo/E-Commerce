using E_Commerce.Application.Interfaces.Modelos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ModeloRepository(ECommerceDBContext context) : IModeloRepository
    {
        private ECommerceDBContext _context = context;

        public async Task AddAsync(Modelo modelo)
            => await _context.AddAsync(modelo);

        public async Task<List<ModeloResponseDTO>> GetAllAsync()
            => await _context.Modelos
            .AsNoTracking()
            .Select(m => new ModeloResponseDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                IdMarca = m.IdMarca,
                Marca = m.Marca.Nombre,
                IdTipoProducto = m.IdTipoProducto,
                TipoProducto = m.TipoProducto.Nombre
            }).ToListAsync();

        public async Task<List<ModeloDetalleDTO>> GetAllMoMaProAsync()
            => await _context.Modelos
            .AsNoTracking()
            .Select(m => new ModeloDetalleDTO
            {
                Id = m.Id,
                ModeloMarcaProducto = m.Marca.Nombre + " " + m.Nombre + " " + m.TipoProducto.Nombre
            }).ToListAsync();

        public async Task<Modelo?> GetByIdAsync(int? id)
            => await _context.Modelos
                    .Include(m => m.Marca)
                    .Include(m => m.TipoProducto)
                    .SingleOrDefaultAsync(m => m.Id == id);

        public async Task<Modelo?> GetByIdAsync(int id)
            => await _context.Modelos
            .Where(m => m.Id == id)
            .SingleOrDefaultAsync();

        public async Task<int> GetIdByNombreAsync(string nombre)
            => await _context.Modelos.AsNoTracking()
            .Where(m => m.Nombre == nombre)
            .Select(m => (int?)m.Id)
            .SingleOrDefaultAsync() ?? 0;
    }
}
