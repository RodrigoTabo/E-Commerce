using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.ProductoVariantes;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ProductoVarianteRepository(ECommerceDBContext context) : IProductoVarianteRepository
    {

        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(ProductoVariante productoVariante)
            => await _context.ProductoVariantes.AddAsync(productoVariante);

        public async Task<ProductoVarianteDTO?> GetByIdAsync(int id)
            => await _context.ProductoVariantes
                .AsNoTracking()
                .Where(pv => pv.Id == id)
                .Select(pv => new ProductoVarianteDTO
                {
                    Id = pv.Id,
                    Precio = pv.Precio,
                    Stock = pv.Stock,
                    Atributos = pv.ProductoAtributoVariantes
                        .Select(pav => new AtributoVarianteDTO
                        {
                            IdAtributo = pav.AtributoValor.IdAtributo,
                            Nombre = pav.AtributoValor.Atributo.Nombre,

                            IdValor = pav.AtributoValor.Id,
                            Valor = pav.AtributoValor.Valor
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

        public async Task<int> GetIdProductoVarianteByCombinaciones(int IdProducto, List<int> atributoValorIds)
        {
            var nueva = atributoValorIds
            .OrderBy(x => x)
            .ToList();

            var variantes = await _context.ProductoVariantes
            .AsNoTracking()
            .Where(pv => pv.IdProducto == IdProducto)
            .Select(pv => new
            {
                pv.Id,
                Atributos = pv.ProductoAtributoVariantes.Select(x => x.IdAtributoValor)
            })
            .ToListAsync();

            foreach (var v in variantes)
            {
                var existente = v.Atributos
                    .OrderBy(x => x)
                    .ToList();

                if (existente.SequenceEqual(nueva))
                    return v.Id;
            }

            return 0;
        }

        public async Task<ProductoVariante?> GetProductoVarianteById(int id)
            => await _context.ProductoVariantes
                .Include(x => x.ProductoAtributoVariantes)
                    .ThenInclude(x => x.AtributoValor)
                        .ThenInclude(x => x.Atributo)
                .Include(x => x.Producto)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<ListProductoVariante>> GetProductoVarianteByIdProducto(int IdProducto)
            => await _context.ProductoVariantes
            .AsNoTracking()
            .Where(pv => pv.IdProducto == IdProducto)
            .Select(pv => new ListProductoVariante
            {
                Id = pv.Id,
                IdProducto = pv.IdProducto,
                Producto = pv.Producto.Nombre,
                Precio = pv.Precio,
                Stock = pv.Stock,
                Atributos = pv.ProductoAtributoVariantes
                    .Select(pav => new AtributoVarianteDTO
                    {
                        Nombre = pav.AtributoValor.Atributo.Nombre,
                        Valor = pav.AtributoValor.Valor
                    })
                    .ToList()
            })
            .ToListAsync();

        public async Task<int> GetStockById(int Id)
            => await _context.ProductoVariantes
            .AsNoTracking()
            .Where(pv => pv.Id == Id)
            .Select(pv => pv.Stock)
            .SingleOrDefaultAsync();

        public async Task<List<ProductoVariante>> ListaProductosVariantesByIds(List<int> IdsProductosVariantes)
            => await _context.ProductoVariantes.AsNoTracking().Where(p => IdsProductosVariantes.Contains(p.Id)).ToListAsync();

        public async Task RemoveByVarianteIdAsync(int idVariante)
        {
            var items = await _context.productoAtributoVariantes
                .Where(x => x.IdProductoVariante == idVariante)
                .ToListAsync();

            _context.productoAtributoVariantes.RemoveRange(items);
        }

    }
}