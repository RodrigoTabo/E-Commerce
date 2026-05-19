using E_Commerce.Application.Interfaces.Ordenes;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.Domicilios;
using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.OrdenItems;
using E_Commerce.Shared.DTOs.Pagos;
using E_Commerce.Shared.DTOs.ProductoVariantes;
using E_Commerce.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace E_Commerce.Infrastructure.Repositories
{
    public class OrdenRepository(ECommerceDBContext context) : IOrdenRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(Orden orden)
            => await _context.Ordenes.AddAsync(orden);

        public async Task<Orden?> GetByIdAsync(int id)
            => await _context.Ordenes
            .Where(o => o.Id == id)
            .SingleOrDefaultAsync();

        public async Task<OrdenDetailsDTO> GetOrdenById(int id)
            => await _context.Ordenes.Where(o => o.Id == id).Select(o => new OrdenDetailsDTO
            {
                Id = o.Id,
                User = o.ApplicationUser.Nombre + " " + o.ApplicationUser.Apellido,
                Domicilio = new DomicilioDTO(
                               o.IdDomicilio,
                               o.CalleSnapshot,
                               o.AlturaSnapshot,
                               o.Domicilio.IdCiudad,
                               o.CiudadSnapshot,
                               o.CodigoPostalSnapshot,
                               null
                           ),
                MetodoEnvio = o.MetodoEnvio.Nombre,
                EstadoOrden = o.EstadoOrden,
                Total = o.Total,
                FechaCreado = o.CreatedAt,
                FechaExpirado = o.ExpiresAt,
                OrdenItemsDetailDTOs = o.OrdenItems.Select(oi => new OrdenItemsDetailDTO
                {
                    Id = oi.Id,
                    IdOrden = oi.IdOrden,
                    ProductoNombre = oi.ProductoVariante.Producto.Nombre,
                    ProductoVariante = new ProductoVarianteDTO
                    {
                        Stock = oi.ProductoVariante.Stock,
                        Atributos = oi.ProductoVariante.ProductoAtributoVariantes
                             .Select(a => new AtributoVarianteDTO
                             {
                                 IdAtributo = a.AtributoValor.Atributo.Id,
                                 Nombre = a.AtributoValor.Atributo.Nombre,
                                 IdValor = a.AtributoValor.Id,
                                 Valor = a.AtributoValor.Valor
                             }).ToList()
                    },
                    Cantidad = oi.Cantidad,
                    PrecioUnitario = oi.PrecioUnitario
                }).ToList(),
                PagoDetailDTO = o.Pagos.Select(p => new PagoDetailDTO
                {
                    Id = p.Id,
                    IdOrden = p.IdOrden,
                    Total = p.Monto,
                    EstadoPago = p.EstadoPago,
                    FechaCreado = p.CreatedAt,
                    ComprobanteUrl = p.ComprobanteUrl,
                    FechaCargaComprobante = p.FechaCargaComprobante
                }).SingleOrDefault()
            }).SingleOrDefaultAsync();


        public async Task<List<ListOrdenDTO>> GetOrdenesAsync()
            => await _context.Ordenes
            .AsNoTracking()
            .Select(o => new ListOrdenDTO
            {
                Id = o.Id,
                User = o.ApplicationUser.Nombre + " " + o.ApplicationUser.Apellido,
                DomicilioCompleto = o.CalleSnapshot + " " + o.AlturaSnapshot + " " + o.CiudadSnapshot + " " + o.CodigoPostalSnapshot,
                MetodoEnvio = o.MetodoEnvio.Nombre,
                EstadoOrden = o.EstadoOrden,
                Total = o.Total,
                FechaCreado = o.CreatedAt,
                FechaExpirado = o.ExpiresAt
            })
            .ToListAsync();

        public async Task<List<OrdenDetailsUserDTO>> ListOrdenByUsers(Guid? UserId)
            => await _context.Ordenes
            .Where(o => o.IdApplicationUser == UserId)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrdenDetailsUserDTO
            {
                Id = o.Id,
                Domicilio = new DomicilioDTO(o.IdDomicilio, o.CalleSnapshot, o.AlturaSnapshot, o.Domicilio.IdCiudad, o.CiudadSnapshot, o.CodigoPostalSnapshot, null),
                MetodoEnvio = o.MetodoEnvio.Nombre,
                EstadoOrden = o.EstadoOrden,
                Total = o.Total,
                FechaCreado = o.CreatedAt,
                FechaExpirado = o.ExpiresAt,
                OrdenItemsDetailUserDTOs = o.OrdenItems.Select(oi => new OrdenItemsDetailUserDTO
                {
                    ProductoNombre = oi.ProductoVariante.Producto.Nombre,
                    ProductoVariante = new ProductoVarianteDTO
                    {
                        Atributos = oi.ProductoVariante.ProductoAtributoVariantes
                             .Select(a => new AtributoVarianteDTO
                             {
                                 IdAtributo = a.AtributoValor.Atributo.Id,
                                 Nombre = a.AtributoValor.Atributo.Nombre,
                                 IdValor = a.AtributoValor.Id,
                                 Valor = a.AtributoValor.Valor
                             }).ToList()
                    },
                    Cantidad = oi.Cantidad,
                    PrecioUnitario = oi.PrecioUnitario
                }).ToList(),
                PagoDetailUserDTO = o.Pagos.Select(p => new PagoDetailUserDTO
                {
                    EstadoPago = p.EstadoPago,
                    ComprobanteUrl = p.ComprobanteUrl,
                    Total = p.Monto,
                }).SingleOrDefault()
            }).ToListAsync();
    }
}
