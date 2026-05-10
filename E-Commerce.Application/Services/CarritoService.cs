using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Application.Interfaces.Carritos;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Carritos;
using ROP;

namespace E_Commerce.Application.Services
{
    public class CarritoService(ICarritoRepository carritoRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        ICurrentUserService currentUserService,
        ICarritoItemsService carritoItemsService,
        IProductoVarianteService productoVarianteService) : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository = carritoRepository;
        private readonly ICarritoItemsService _carritoItemsService = carritoItemsService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IProductoVarianteService _productoVarianteService = productoVarianteService;

        public async Task<Result<int>> AgregarAlCarrito(AgregarCarritoDTO request)
        {
            //1. Agarramos userId del contexto.
            var userId = _currentUserService.UserId;
            //2. Retornamos si no esta autorizado.
            if (userId is null)
                return Result.Conflict<int>("No estás autorizado");
            //3. Verificamos si el user tiene un carrito.
            var carritoResult = await ObtenerOCrearCarrito(userId.Value);
            //4. Retornamos errores en caso de que haya.
            if (!carritoResult.Success)
                return Result.Failure<int>(carritoResult.Errors);
            //5. Servicio CarritoItems para guardar productos.
            var itemResult = await _carritoItemsService.AgregarCarritoItems(request, carritoResult.Value);
            //6. Retornamos error en el caso de nos retorne un error.
            if (!itemResult.Success)
                return Result.Failure<int>(itemResult.Errors);
            //7.Retornamos exito.
            return Result.Success(itemResult.Value.Id);
        }

        public async Task<Result<CarritoDto?>> CarritoUser()
        {
            //1. Obtener userId del contexto.
            var userId = _currentUserService.UserId;
            //2. Retornamos si no esta autorizado.
            if (userId is null)
                return Result.Conflict<CarritoDto?>("No estás autorizado");
            //3.Obtenemos el carrito del user.
            var carritoUser = await _carritoRepository.CarritoUser(userId);
            //4. Retornar error si no existe. ¡¡¡ATENCIÓN; ARREGLAR ESTO!!!
            if (!carritoUser.Success)
                return Result.Failure<CarritoDto?>(carritoUser.Errors);
            //5. Retornar Carrito obtenido.
            return Result.Success(carritoUser.Value);
        }

        public async Task<Result<CarritoDto?>> RestarCantidad(int IdProductoVariante)
        {
            //1. Obtener userId del contexto.
            var userId = _currentUserService.UserId;
            //2. Retornamos si no esta autorizado.
            if (userId is null)
                return Result.Conflict<CarritoDto?>("No estás autorizado");
            //3. Obtenemos el productoVariante del carrito
            var obtenerItemCarrito = await _carritoItemsService.ObtenerItemCarrito(IdProductoVariante, userId);
            //4. Si no existe, retornamos error.
            if (!obtenerItemCarrito.Success)
                return Result.Failure<CarritoDto?>(obtenerItemCarrito.Errors);
            //5. Si existe y es mayor a uno, descontamos de a uno, si es igual a uno, se lo removemos.
            if (obtenerItemCarrito.Value.Cantidad > 1)
            {
                obtenerItemCarrito.Value.Cantidad -= 1;
            }
            else if (obtenerItemCarrito.Value.Cantidad == 1)
            {
                await _carritoItemsService.RemoverProducto(obtenerItemCarrito.Value);
            }
            //6. Guardamos y retornamos.
            await _unitOfWorkRepository.SaveChangesAsync();
            return await CarritoUser();
        }

        public async Task<Result<CarritoDto?>> SumarCantidad(int IdProductoVariante)
        {
            //1. Obtener userId del contexto.
            var userId = _currentUserService.UserId;
            //2. Retornamos si no esta autorizado.
            if (userId is null)
                return Result.Conflict<CarritoDto?>("No estás autorizado");
            //3.Obtenemos el producto del carrito
            var obtenerItemCarrito = await _carritoItemsService.ObtenerItemCarrito(IdProductoVariante, userId);
            //4. Si retorna error, lo retornamos
            if (!obtenerItemCarrito.Success)
                return Result.Failure<CarritoDto?>(obtenerItemCarrito.Errors);

            //5. Obtenemos el producto
            var stockProductoVariante = await _productoVarianteService.GetStockById(IdProductoVariante);
            //6. Si retorna error, lo retornamos. 
            if (!stockProductoVariante.Success)
                return Result.Failure<CarritoDto?>(stockProductoVariante.Errors);
            //7. Modificamos la cantidad
            var nuevaCantidad = obtenerItemCarrito.Value.Cantidad + 1;
            //8. Si la nueva cantidad, supera el stock, retornamos el error.
            if (stockProductoVariante.Value < nuevaCantidad)
                return Result.Conflict<CarritoDto?>("No hay stock suficiente");
            //9. Si no la supera, actualizamos el carrito, guardamos y retornamos.
            obtenerItemCarrito.Value.Cantidad = nuevaCantidad;

            await _unitOfWorkRepository.SaveChangesAsync();
            return await CarritoUser();
        }

        //Metodos privado
        private async Task<Result<int>> ObtenerOCrearCarrito(Guid userId)
        {
            //1. Obtener CarritoId del usuario logeado.
            var carritoId = await _carritoRepository.ObtenerPorUsuario(userId);

            //2. Si existe, retornamos, sino, creamos
            if (carritoId > 0)
                return Result.Success(carritoId);

            var carrito = new Carrito
            {
                IdApplicationUser = userId
            };
            //3. Si no existe, guardamos y retornamos el Id.
            await _carritoRepository.AddAsync(carrito);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(carrito.Id);
        }

    }
}
