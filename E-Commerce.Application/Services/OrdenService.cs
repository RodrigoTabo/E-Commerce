using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.MetodoEnvios;
using E_Commerce.Application.Interfaces.Ordenes;
using E_Commerce.Application.Interfaces.OrdenItems;
using E_Commerce.Application.Interfaces.Pagos;
using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.Pagos;
using ROP;

namespace E_Commerce.Application.Services
{
    public class OrdenService(IOrdenRepository ordenRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        IMetodoEnvioService metodoEnvioService,
        IDomicilioService domicilioService,
        ICurrentUserService currentUserService,
        ICarritoItemsService carritoItemsService,
        IProductoService productoService,
        IOrdenItemService ordenItemService,
        IPagoService pagoService) : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository = ordenRepository;
        private readonly IOrdenItemService _ordenItemService = ordenItemService;
        private readonly IMetodoEnvioService _metodoEnvioService = metodoEnvioService;
        private readonly IDomicilioService _domicilioService = domicilioService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IProductoService _productoService = productoService;
        private readonly ICarritoItemsService _carritoItemsService = carritoItemsService;
        private readonly IPagoService _pagoService = pagoService;

        public async Task<Result<int>> CreateAsync(CreateOrdenRequest request, PagoRequestDTO pagorequest)
        {
            await _unitOfWorkRepository.BeginTransactionAsync();
            try
            {
                var userId = _currentUserService.UserId;

                if (userId is null)
                    return Result.Conflict<int>("Debes conectarte para esta acción.");

                var carritoItems = await _carritoItemsService.ObtenerItemCarritoByIdUser(userId);

                if (!carritoItems.Success)
                    return Result.Failure<int>(carritoItems.Errors);

                if (!carritoItems.Value.Any())
                    return Result.BadRequest<int>("Carrito vacío.");

                var productosIds = carritoItems.Value
                    .Select(x => x.IdProductoVariante)
                    .Distinct()
                    .ToList();

                var productosResult = await _productoService.ListaProductosByIds(productosIds);

                if (!productosResult.Success)
                    return Result.Failure<int>(productosResult.Errors);

                var productos = productosResult.Value;
                var items = carritoItems.Value;

                var crearOrden = await CrearOrden(request, productos, items, userId);
                if (!crearOrden.Success)
                    return Result.Failure<int>(crearOrden.Errors);

                var ordenItemsResult = _ordenItemService.CrearOrdenItem(crearOrden.Value, productos, items);
                if (!ordenItemsResult.Success)
                    return Result.Failure<int>(ordenItemsResult.Errors);

                var pagoPendiente = await _pagoService.CrearPagoAsync(crearOrden.Value, pagorequest);
                if (!pagoPendiente.Success)
                    return Result.Failure<int>(pagoPendiente.Errors);

                //!!!!!!! MOVER ESTE METODO A CONFIRMAR PAGO.
                //var stockResult = _productoService.DescontarStock(productos, items);
                //if (!stockResult.Success)
                //    return Result.Failure<int>(stockResult.Errors);
                crearOrden.Value.OrdenItems = ordenItemsResult.Value;

                await _unitOfWorkRepository.SaveChangesAsync();

                await _unitOfWorkRepository.CommitAsync();

                return Result.Success(crearOrden.Value.Id);
            }
            catch
            {
                await _unitOfWorkRepository.RollbackAsync();
                throw;
            }
        }

        private async Task<Result<Orden>> CrearOrden(CreateOrdenRequest request, List<Producto> productos, List<CarritoItem> carritoitems, Guid? userId)
        {

            Domicilio? domicilio = null;

            var validarDTO = ValidarDTO(request);
            if (!validarDTO.Success)
                return Result.Failure<Orden>(validarDTO.Errors);

            var validarIdMetodo = await _metodoEnvioService.MetodoEnvioExistente(request.IdMetodoEnvio);
            if (!validarIdMetodo.Success)
                return Result.NotFound<Orden>(validarIdMetodo.Errors);

            if (request.IdMetodoEnvio == 1)
            {
                var validarIdDomicilio = await _domicilioService.ValidarDomicilioExistente(request.IdDomicilio);
                if (!validarIdDomicilio.Success)
                    return Result.NotFound<Orden>(validarIdDomicilio.Errors);
                domicilio = validarIdDomicilio.Value;
            }

            var productosDict = productos
                .ToDictionary(p => p.Id);

            decimal total = 0;

            foreach (var item in carritoitems)
            {
                if (!productosDict.TryGetValue(item.IdProductoVariante, out var producto))
                    return Result.Failure<Orden>($"Producto {item.IdProductoVariante} no existe.");

                //total += producto.Precio * item.Cantidad;
            }

            var orden = new Orden
            {
                IdApplicationUser = userId,
                IdMetodoEnvio = request.IdMetodoEnvio,
                Total = total,
                EstadoOrden = Shared.Enums.EstadoOrden.PendientePago,
            };

            if (request.IdMetodoEnvio == 1)
            {
                orden.IdDomicilio = domicilio.Id;
                orden.CalleSnapshot = domicilio.Calle;
                orden.AlturaSnapshot = domicilio.Altura;
                orden.CiudadSnapshot = domicilio.Ciudad.Nombre
                    + domicilio.Ciudad.Provincia.Nombre
                    + domicilio.Ciudad.Provincia.Pais.Nombre;
                orden.CodigoPostalSnapshot = domicilio.CodigoPostal;
            }

            await _ordenRepository.AddAsync(orden);

            return Result.Success(orden);
        }

        private Result<Unit> ValidarDTO(CreateOrdenRequest request)
        {
            if (request.IdMetodoEnvio <= 0)
                return Result.BadRequest<Unit>("Debes seleccionar un metodo de envio.");

            if (request.IdMetodoEnvio == 1 && request.IdDomicilio <= 0)
                return Result.BadRequest<Unit>("Debes seleccionar un punto de entrega.");
            if (request.IdMetodoEnvio == 1 && request.IdDomicilio > 0 && request.AlturaSnapshot < 0
                && string.IsNullOrWhiteSpace(request.CiudadSnapshot)
                && string.IsNullOrWhiteSpace(request.CalleSnapshot)
                && string.IsNullOrWhiteSpace(request.CodigoPostaSnapshot))
                return Result.BadRequest<Unit>("No se han cargado los datos adicionales.");
            //if (request.Total <= 0)
            //    return Result.BadRequest<Unit>("Ha fallado el calculo Total.");

            return Result.Success();
        }
    }
}
