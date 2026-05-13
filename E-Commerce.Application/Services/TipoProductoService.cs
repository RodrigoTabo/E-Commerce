using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.TipoProductos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.TipoProducto;
using ROP;

namespace E_Commerce.Application.Services
{
    public class TipoProductoService(ITipoProductoRepository tipoProductoRepository, IUnitOfWorkRepository unitOfWorkRepository) : ITipoProductoService
    {
        private readonly ITipoProductoRepository _tipoProductoRepository = tipoProductoRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<int>> CreateAsync(CreateTipoProductoDTO request)
        {
            //1. Validamos que haya ingresado nombre.
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return Result.BadRequest<int>("El nombre es obligatorio.");

            //2. Validamos que el tipo de producto no exista.
            var id = await _tipoProductoRepository.GetIdByNombreAsync(request.Nombre);
            if (id != 0)
                return Result.Conflict<int>("El tipo de producto ya existe.");

            //3. Creamos el objeto, lo guardamos y lo retornamos Id.
            var newTipoProducto = new TipoProducto
            {
                Nombre = request.Nombre
            };

            await _tipoProductoRepository.AddAsync(newTipoProducto);
            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success(newTipoProducto.Id);
        }

        public async Task<Result<List<TipoProductoDTO>>> GetAllAsync()
        {
            var lista = await _tipoProductoRepository.GetAllAsync();
            if (!lista.Any())
                return Result.Conflict<List<TipoProductoDTO>>("La lista esta vacia");

            return Result.Success(lista);
        }

        public async Task<Result<Unit>> UpdateAsync(TipoProductoDTO request)
        {

            //1. Validamos que el campo que ingreso sea valido
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return Result.BadRequest<Unit>("El nombre es obligatorio.");

            //2. Traemos el opbjeto y validamos su existencia.
            var tipoProducto = await _tipoProductoRepository.GetByIdAsync(request.Id);

            if (tipoProducto is null)
                return Result.NotFound<Unit>("El tipo de producto no existe");

            //3. Validamos que el nombre que ingreso no exista.
            var id = await _tipoProductoRepository.GetIdByNombreAsync(request.Nombre);

            if (id != 0 && id != request.Id)
                return Result.Conflict<Unit>("El tipo de producto ya existe.");

            //4. Guardamos los cambios.
            tipoProducto.Nombre = request.Nombre;
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<bool> ValidarByIdAsync(int id)
        {
            var tipoProducto = await _tipoProductoRepository.GetByIdAsync(id);

            if (tipoProducto is null)
                return false;

            return true;
        }
    }
}
