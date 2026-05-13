using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Productos;
using E_Commerce.Shared.DTOs.ProductoVariantes;
using E_Commerce.Shared.DTOs.Reviews;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ProductoRepository(ECommerceDBContext context) : IProductoRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Producto producto)
            => await _context.Productos.AddAsync(producto);

        public async Task<List<Producto>> GetAllAsync()
            => await _context.Productos
                .AsNoTracking()
                .Include(p => p.Modelo)
                    .ThenInclude(m => m.Marca)
                .Include(p => p.Modelo)
                    .ThenInclude(m => m.TipoProducto)
                .Include(p => p.ApplicationUser)
                .ToListAsync();

        public async Task<ProductoDetalleDTO?> GetProductoDetalleAsync(int id)
            => await _context.Productos
                    .Where(p => p.Id == id)
                    .Select(p => new ProductoDetalleDTO
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        UrlImagen = p.UrlImagen,
                        Descripcion = p.Descripcion,
                        CategoriaNombre = p.Modelo.TipoProducto.Nombre,
                        MarcaNombre = p.Modelo.Marca.Nombre,
                        Modelo = p.Modelo.Nombre,
                        NombreVendedor = p.ApplicationUser.Nombre + " " + p.ApplicationUser.Apellido,
                        FechaPublicado = p.CreatedAt,
                        ReviewDTO = p.Reviews.Select(r => new ReviewDTO
                        {
                            Id = r.Id,
                            comentario = r.Comentario,
                            rating = r.Rating,
                            UsuarioId = r.IdApplicationUser,
                            UsuarioNombre = r.ApplicationUser.Nombre + r.ApplicationUser.Apellido,
                            CreateAt = r.CreatedAt
                        }).ToList(),
                        PromedioEstrellas = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
                        TotalReviews = p.Reviews.Count(),
                        Variantes = p.ProductoVariantes.Select(pv => new ProductoVarianteDTO
                        {
                            Id = pv.Id,
                            Precio = pv.Precio,
                            Stock = pv.Stock,
                            Atributos = pv.ProductoAtributoVariantes.Select(av => new AtributoVarianteDTO
                            {
                                IdValor = av.AtributoValor.Id,
                                Nombre = av.AtributoValor.Atributo.Nombre,
                                IdAtributo = av.AtributoValor.Atributo.Id,
                                Valor = av.AtributoValor.Valor
                            })
                            .ToList()
                        })
                        .ToList()
                    }).SingleOrDefaultAsync();

        public async Task<Producto?> GetProductoByIdAsync(int id)
            => await _context.Productos.Where(p => p.Id == id).SingleOrDefaultAsync();

        public async Task<List<Producto>> ListaProductosByIds(List<int> IdsProductos)
            => await _context.Productos.AsNoTracking().Where(p => IdsProductos.Contains(p.Id)).ToListAsync();
    }
}
