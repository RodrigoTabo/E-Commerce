using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Application.Interfaces.Carritos;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Carritos;
using ROP;

namespace E_Commerce.Application.Services
{
    public class CarritoService(ICarritoRepository carritoRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        ICurrentUserService currentUserService,
        ICarritoItemsService carritoItemsService,
        IProductoService productoService) : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository = carritoRepository;
        private readonly ICarritoItemsService _carritoItemsService = carritoItemsService;
        private readonly IProductoService _productoService = productoService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<int>> AgregarAlCarrito(AgregarCarritoDTO request)
        {
            var userId = _currentUserService.UserId;

            if (userId is null)
                return Result.Conflict<int>("No estás autorizado");

            var carritoResult = await ObtenerOCrearCarrito(userId.Value);

            if (!carritoResult.Success)
                return Result.Failure<int>(carritoResult.Errors);

            var itemResult = await _carritoItemsService
                .AgregarCarritoItems(request, carritoResult.Value);

            if (!itemResult.Success)
                return Result.Failure<int>(itemResult.Errors);

            return Result.Success(itemResult.Value.Id);
        }

        public async Task<Result<CarritoDto?>> CarritoUser()
        {
            var userId = _currentUserService.UserId;

            if (userId is null)
                return Result.Conflict<CarritoDto?>("No estás autorizado");

            var carritoUser = await _carritoRepository.CarritoUser(userId);

            if (!carritoUser.Success)
                return Result.Failure<CarritoDto?>(carritoUser.Errors);

            return Result.Success(carritoUser.Value);
        }

        public async Task<Result<CarritoDto?>> RestarCantidad(int IdProducto)
        {
            var userId = _currentUserService.UserId;

            var obtenerItemCarrito = await _carritoItemsService.ObtenerItemCarrito(IdProducto, userId);

            if (!obtenerItemCarrito.Success)
                return Result.Failure<CarritoDto?>(obtenerItemCarrito.Errors);

            if (obtenerItemCarrito.Value.Cantidad > 1)
            {
                obtenerItemCarrito.Value.Cantidad -= 1;
            }
            else if (obtenerItemCarrito.Value.Cantidad == 1)
            {
                await _carritoItemsService.RemoverProducto(obtenerItemCarrito.Value);
            }

            await _unitOfWorkRepository.SaveChangesAsync();
            return await CarritoUser();
        }

        public async Task<Result<CarritoDto?>> SumarCantidad(int IdProducto)
        {
            var userId = _currentUserService.UserId;

            var obtenerItemCarrito = await _carritoItemsService.ObtenerItemCarrito(IdProducto, userId);
            var productoStock = await _productoService.GetByIdAsync(IdProducto);

            if (!obtenerItemCarrito.Success)
                return Result.Failure<CarritoDto?>(obtenerItemCarrito.Errors);

            if (!productoStock.Success)
                return Result.Failure<CarritoDto?>(productoStock.Errors);

            var nuevaCantidad = obtenerItemCarrito.Value.Cantidad + 1;

            if (productoStock.Value.Stock < nuevaCantidad)
                return Result.Conflict<CarritoDto?>("No hay stock suficiente");

            obtenerItemCarrito.Value.Cantidad = nuevaCantidad;

            await _unitOfWorkRepository.SaveChangesAsync();

            return await CarritoUser();
        }

        //Metodos privado
        private async Task<Result<int>> ObtenerOCrearCarrito(Guid userId)
        {
            var carritoId = await _carritoRepository.ObtenerPorUsuario(userId);

            if (carritoId > 0)
                return Result.Success(carritoId);

            var carrito = new Carrito
            {
                IdApplicationUser = userId
            };

            await _carritoRepository.AddAsync(carrito);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(carrito.Id);
        }

    }
}
