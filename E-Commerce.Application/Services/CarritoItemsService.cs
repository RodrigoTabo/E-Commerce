using E_Commerce.Application.Interfaces.CarritoItems;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Carritos;
using ROP;

namespace E_Commerce.Application.Services
{
    public class CarritoItemsService(ICarritoItemsRepository carritoItemsRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        IProductoVarianteService productoVarianteService) : ICarritoItemsService
    {
        private ICarritoItemsRepository _carritoItemsRepository = carritoItemsRepository;
        private IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private IProductoVarianteService _productoVarianteService = productoVarianteService;

        public async Task<Result<CarritoItem>> AgregarCarritoItems(AgregarCarritoDTO dto, int carritoId)
        {

            //Obtenemos el producto variante para validar.
            var productoVariante = await _productoVarianteService.GetProductoVarianteById(dto.IdProductoVariante);

            if (!productoVariante.Success)
                return Result.NotFound<CarritoItem>("La variante del producto no existe");

            //Obtenemos el carritoItem si existe, sino, lo creamos.
            var item = await _carritoItemsRepository.ValidarCarritoItemExistente(dto.IdProductoVariante, carritoId);

            //Si el productovariante es nuevo, le asignamos el stock seleccionado
            //Si ya existe y modifico, le agregamos el nuevo stock seleccionado.
            int cantidadFinal = item is null ? dto.Stock : item.Cantidad + dto.Stock;

            //Si no hay stock, lanzamos error.
            if (productoVariante.Value.Stock < cantidadFinal)
                return Result.BadRequest<CarritoItem>("No hay stock suficiente");

            //Con esto nos aseguramos de que actualice el objeto dependiendo si existe o no.
            //Si existe, lo actualiza, sino, lo crea.
            if (item is not null)
            {
                item.Cantidad = cantidadFinal;
            }
            else
            {
                item = new CarritoItem
                {
                    IdCarrito = carritoId,
                    IdProductoVariante = dto.IdProductoVariante,
                    Cantidad = dto.Stock,
                    Precio = productoVariante.Value.Precio
                };
                //Agregamos el productoVariante a la DB.
                await _carritoItemsRepository.AgregarProductoVarianteEnCarritoItem(item);
            }
            //Lo guardamos y retornamos el item.
            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success(item);
        }

        public async Task<Result<CarritoItem?>> ObtenerItemCarrito(int IdProductoVariante, Guid? userId)
        {
            //1. Obtenemos los items del carrito del user.
            var carritoItem = await _carritoItemsRepository.ObtenerItemCarrito(IdProductoVariante, userId);
            //2. Retornamos error no existen.
            if (!carritoItem.Success)
                return Result.NotFound<CarritoItem?>("La variante del producto no existe");
            //3. Retornamos el valor si existe.
            return Result.Success(carritoItem.Value);

        }

        public async Task<Result<List<CarritoItem?>>> ObtenerItemCarritoByIdUser(Guid? userId)
        {
            var obtenerCarritoByIdUser = await _carritoItemsRepository.ObtenerItemCarritoByIdUser(userId);

            if (obtenerCarritoByIdUser is null)
                return Result.Conflict<List<CarritoItem?>>("El carrito de los productos no existe.");

            return Result.Success(obtenerCarritoByIdUser);
        }

        //Removemos los items del carrito padre.
        public async Task RemoverProducto(CarritoItem request)
            => await _carritoItemsRepository.RemoverProducto(request);
    }
}
