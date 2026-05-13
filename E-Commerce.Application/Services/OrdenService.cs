using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Application.Interfaces.Domicilios;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.MetodoEnvios;
using E_Commerce.Application.Interfaces.Ordenes;
using E_Commerce.Application.Interfaces.OrdenItems;
using E_Commerce.Application.Interfaces.Pagos;
using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.Pagos;
using E_Commerce.Shared.Enums;
using ROP;

namespace E_Commerce.Application.Services
{
    public class OrdenService(IOrdenRepository ordenRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        IMetodoEnvioService metodoEnvioService,
        IDomicilioService domicilioService,
        ICurrentUserService currentUserService,
        ICarritoItemsService carritoItemsService,
        IProductoVarianteService productoVarianteService,
        IOrdenItemService ordenItemService,
        IPagoService pagoService) : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository = ordenRepository;
        private readonly IOrdenItemService _ordenItemService = ordenItemService;
        private readonly IMetodoEnvioService _metodoEnvioService = metodoEnvioService;
        private readonly IDomicilioService _domicilioService = domicilioService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IProductoVarianteService _productoVarianteService = productoVarianteService;
        private readonly ICarritoItemsService _carritoItemsService = carritoItemsService;
        private readonly IPagoService _pagoService = pagoService;

        public async Task<Result<Unit>> AprobarPagoAsync(int ordenId)
        {
            //1. Buscar si la orden existe y obtener el objeto para modificar estado.
            var orden = await _ordenRepository.GetByIdAsync(ordenId);
            if (orden is null)
                return Result.NotFound<Unit>("La orden no existe.");

            //2. Buscar si el pago enlazado a la orden existe y obtener el objeto para modificar estado.
            var pago = await _pagoService.GetPagoByOrdenId(orden.Id);
            if (!pago.Success)
                return Result.Failure<Unit>(pago.Errors);

            //3. Buscar los estados de ambos, si estan en estado pendiente,
            if (!(pago.Value.EstadoPago == EstadoPago.Procesando && orden.EstadoOrden == EstadoOrden.Procesando))
                return Result.Conflict<Unit>("La orden no se encuentra en un estado válido para aprobar el pago.");

            //4.Cambiamos estados y guardamos.
            var now = DateTime.UtcNow;
            pago.Value.EstadoPago = EstadoPago.Aprobado;
            pago.Value.UpdatedAt = now;
            orden.EstadoOrden = EstadoOrden.Pagada;
            orden.UpdatedAt = now;

            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<Unit>> PrepararOrdenAsync(int ordenId)
        {
            //1. Buscar si la orden existe y obtener el objeto para modificar estado y validar.
            var orden = await _ordenRepository.GetByIdAsync(ordenId);
            if (orden is null)
                return Result.NotFound<Unit>("La orden no existe.");

            //2. Buscar si el pago enlazado a la orden existe para validar Pago Aprobado.
            var pago = await _pagoService.GetPagoByOrdenId(orden.Id);
            if (!pago.Success)
                return Result.Failure<Unit>(pago.Errors);

            //3. Buscar los estados de ambos, si estan en estado aprobados,
            if (!(pago.Value.EstadoPago == EstadoPago.Aprobado && orden.EstadoOrden == EstadoOrden.Pagada))
                return Result.Conflict<Unit>("La orden no se encuentra en un estado válido para preparar el pedido.");

            //4.Cambiamos estados y guardamos.
            var now = DateTime.UtcNow;
            orden.EstadoOrden = EstadoOrden.Preparando;
            orden.UpdatedAt = now;

            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<Unit>> EnviarOrdenAsync(int ordenId)
        {
            //1. Buscar si la orden existe y obtener el objeto para modificar estado y validar.
            var orden = await _ordenRepository.GetByIdAsync(ordenId);
            if (orden is null)
                return Result.NotFound<Unit>("La orden no existe.");

            //2. Buscar si el pago enlazado a la orden existe para validar Pago Aprobado.
            var pago = await _pagoService.GetPagoByOrdenId(orden.Id);
            if (!pago.Success)
                return Result.Failure<Unit>(pago.Errors);

            //3. Buscar los estados de ambos, si estan en estado aprobado y preparados,
            if (!(pago.Value.EstadoPago == EstadoPago.Aprobado && orden.EstadoOrden == EstadoOrden.Preparando))
                return Result.Conflict<Unit>("La orden no se encuentra en un estado válido para enviar el pedido.");

            //4.Cambiamos estados y guardamos.
            var now = DateTime.UtcNow;
            orden.EstadoOrden = EstadoOrden.Enviada;
            orden.UpdatedAt = now;

            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<Unit>> EntregarOrdenAsync(int ordenId)
        {
            //1. Buscar si la orden existe y obtener el objeto para modificar estado y validar.
            var orden = await _ordenRepository.GetByIdAsync(ordenId);
            if (orden is null)
                return Result.NotFound<Unit>("La orden no existe.");

            //2. Buscar si el pago enlazado a la orden existe para validar Pago Aprobado.
            var pago = await _pagoService.GetPagoByOrdenId(orden.Id);
            if (!pago.Success)
                return Result.Failure<Unit>(pago.Errors);

            //3. Buscar los estados de ambos, si estan en estado aprobado y enviada,
            if (!(pago.Value.EstadoPago == EstadoPago.Aprobado && orden.EstadoOrden == EstadoOrden.Enviada))
                return Result.Conflict<Unit>("La orden no se encuentra en un estado válido para entegar el pedido.");

            //4.Cambiamos estados y guardamos.
            var now = DateTime.UtcNow;
            orden.EstadoOrden = EstadoOrden.Entregada;
            orden.UpdatedAt = now;

            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<int>> CreateAsync(CreateOrdenRequest request, PagoRequestDTO pagorequest)
        {
            await _unitOfWorkRepository.BeginTransactionAsync();
            try
            {
                //1. Traemos el usuario logeado del contexto.
                var userId = _currentUserService.UserId;

                //2. Si no existe, retornamos error.
                if (userId is null)
                    return Result.Conflict<int>("Debes conectarte para esta acción.");

                //3. Traemos el carrito mediante userId.
                var carritoItems = await _carritoItemsService.ObtenerItemCarritoByIdUser(userId);

                //4. Si no tiene carrito, retornamos error.
                if (!carritoItems.Success)
                    return Result.Failure<int>(carritoItems.Errors);

                //5. Si el carrito esta vacio, le retornamos aviso.
                if (!carritoItems.Value.Any())
                    return Result.BadRequest<int>("Carrito vacío.");

                //6. Seleccionamos los IdProductosVariantes y los distingimos en una lista.
                var productosIds = carritoItems.Value
                    .Select(x => x.IdProductoVariante)
                    .Distinct()
                    .ToList();

                //7. Buscamos los productos mediante la lista de Ids anterior.
                var productoVarianteResult = await _productoVarianteService.ListaProductosVariantesByIds(productosIds);

                //8. Si el resultado arroja error, lo retornamos.
                if (!productoVarianteResult.Success)
                    return Result.Failure<int>(productoVarianteResult.Errors);

                //9. Guardamos todo en variables.
                var productoVariante = productoVarianteResult.Value;
                var items = carritoItems.Value;

                //10. Creamos la orden del usuario
                var crearOrden = await CrearOrden(request, productoVariante, items, userId);

                //11. Si la orden fallo, retornamos y cortamos todo.
                if (!crearOrden.Success)
                    return Result.Failure<int>(crearOrden.Errors);

                //12. Creamos las ordenes de items
                var ordenItemsResult = _ordenItemService.CrearOrdenItem(crearOrden.Value, productoVariante, items);

                //13. Si tira error retornamos.
                if (!ordenItemsResult.Success)
                    return Result.Failure<int>(ordenItemsResult.Errors);

                //14. Colocamos el pago en pendiente para que el usuario cargue el comprobante.
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

        public async Task<Result<OrdenDetailsDTO>> GetOrdenById(int id)
        {
            var orden = await _ordenRepository.GetOrdenById(id);
            if (orden is null)
                return Result.NotFound<OrdenDetailsDTO>("No existe la orden.");

            return Result.Success(orden);
        }

        public async Task<Result<List<ListOrdenDTO>>> GetOrdenesAsync()
        {
            var ordenes = await _ordenRepository.GetOrdenesAsync();
            if (ordenes is null || !ordenes.Any())
                return Result.NotFound<List<ListOrdenDTO>>("La lista esta vacia.");

            return Result.Success(ordenes);
        }

        //Metodos privados

        private async Task<Result<Orden>> CrearOrden(CreateOrdenRequest request, List<ProductoVariante> productoVariantes, List<CarritoItem> carritoitems, Guid? userId)
        {
            //Variable en caso que el metodo de envio sea DOMICILIO.
            Domicilio? domicilio = null;

            //1. Validar campos cargados.
            var validarDTO = ValidarDTO(request);

            //2. Retornamos errores de validaciones
            if (!validarDTO.Success)
                return Result.Failure<Orden>(validarDTO.Errors);

            //3. Valida Metodo Envio existente.
            var validarIdMetodoEnvio = await _metodoEnvioService.MetodoEnvioExistente(request.IdMetodoEnvio);

            //4. Retornamos errores de metodos envio
            if (!validarIdMetodoEnvio.Success)
                return Result.NotFound<Orden>(validarIdMetodoEnvio.Errors);

            //5. Si el metodo envio es domicilio
            if (request.IdMetodoEnvio == 1)
            {
                //6. Validamos el domicilio seleccionado.
                var validarIdDomicilio = await _domicilioService.ValidarDomicilioExistente(request.IdDomicilio);
                //7. Retornamos errores de domicilio.

                if (!validarIdDomicilio.Success)
                    return Result.NotFound<Orden>(validarIdDomicilio.Errors);
                //8. Guardamos en la variable el obj domicilio.
                domicilio = validarIdDomicilio.Value;
            }
            //9. Obtenemos una lista de Ids cargados en el carritoItem y lo convertimos en diccionario.
            var productoVariantesDict = productoVariantes.ToDictionary(p => p.Id);
            //10. variable para calcular total.
            decimal total = 0;
            //11. recorremos el carritoitem, seleccionamos el Idproductovariante para validar que exista en el carrito.
            //calculamos el precio de la DB del productovariante y lo multiplicamos por la cantidad que eligio el usuario, esto evita romper precios del front.
            foreach (var item in carritoitems)
            {
                if (!productoVariantesDict.TryGetValue(item.IdProductoVariante, out var productovariante))
                    return Result.Failure<Orden>($"Variante {item.IdProductoVariante} no existe.");

                total += productovariante.Precio * item.Cantidad;
            }
            //12. Creamos la nueva orden.
            var orden = new Orden
            {
                IdApplicationUser = userId,
                IdMetodoEnvio = request.IdMetodoEnvio,
                Total = total,
                EstadoOrden = Shared.Enums.EstadoOrden.PendientePago,
            };
            //13. Si el metodo es domicilio, cargamos los datos del domicilio en la orden.
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
            //14.Guardamos y devolvemos.
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
            if (request.Total <= 0)
                return Result.BadRequest<Unit>("Ha fallado el calculo Total.");

            return Result.Success();
        }
    }
}
