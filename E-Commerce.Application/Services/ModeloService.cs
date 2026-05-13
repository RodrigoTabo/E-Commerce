using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Marcas;
using E_Commerce.Application.Interfaces.Modelos;
using E_Commerce.Application.Interfaces.TipoProductos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Modelos;
using ROP;

namespace E_Commerce.Application.Services
{
    public class ModeloService(IModeloRepository modeloRepository,
        IMarcaService marcaService,
        ITipoProductoService tipoProductoService,
        IUnitOfWorkRepository unitOfWorkRepository) : IModeloService
    {
        private readonly IModeloRepository _modeloRepository = modeloRepository;
        private readonly IMarcaService _marcaService = marcaService;
        private readonly ITipoProductoService _tipoProductoService = tipoProductoService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<List<ModeloDetalleDTO>>> GetAllMoMaProAsync()
        {
            var list = await _modeloRepository.GetAllMoMaProAsync();
            if (!list.Any())
                return Result.Conflict<List<ModeloDetalleDTO>>("La lista esta vacia");

            return Result.Success(list);
        }

        public async Task<Result<ModeloResponseDTO>> GetByIdAsync(int? id)
        {
            var modelo = await _modeloRepository.GetByIdAsync(id);

            var validation = validateModelos(modelo);
            if (!validation.Success)
                return Result.Failure<ModeloResponseDTO>(validation.Errors);


            var dto = MapToResponseDTO(validation.Value!);

            return Result.Success(dto);
        }

        public async Task<Result<Modelo>> GetModeloByIdAsync(int? id)
        {
            var modelo = await _modeloRepository.GetByIdAsync(id);
            if (modelo is null)
                return Result.NotFound<Modelo>("El Modelo no existe");

            return Result.Success(modelo);
        }

        //METODOS PRIVADOS

        private Result<Modelo?> validateModelos(Modelo? modelo)
        {
            if (modelo is null)
                return Result.NotFound<Modelo?>("El Modelo no existe");

            return Result.Success(modelo);
        }

        private ModeloResponseDTO MapToResponseDTO(Modelo m)
        {
            return new ModeloResponseDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Marca = m.Marca?.Nombre ?? "Sin Marca",
                TipoProducto = m.TipoProducto?.Nombre ?? "Sin Tipo"
            };
        }


        //CRUDS


        public async Task<Result<List<ModeloResponseDTO>>> GetAllAsync()
        {
            var lista = await _modeloRepository.GetAllAsync();
            if (!lista.Any())
                return Result.Conflict<List<ModeloResponseDTO>>("La lista esta vacia.");

            return Result.Success(lista);
        }

        public async Task<Result<int>> CreateAsync(CreateModeloDTO request)
        {
            //1. Validamos que haya cargado nombre
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return Result.BadRequest<int>("El nombre es obligatorio.");

            if (request.IdMarca <= 0)
                return Result.BadRequest<int>("La marca es obligatorio.");

            if (request.IdTipoProducto <= 0)
                return Result.BadRequest<int>("El tipo de producto es obligatorio.");

            //2. Validamos que el modelo no exista.
            var id = await _modeloRepository.GetIdByNombreAsync(request.Nombre);
            if (id != 0)
                return Result.Conflict<int>("El modelo ya existe.");

            //3. Validamos que la marca exista
            var marcaExiste = await _marcaService.ValidarByIdAsync(request.IdMarca);
            if (!marcaExiste)
                return Result.NotFound<int>("La marca no existe.");

            //4. Validamos que el tipo de producto exista
            var tipoProductoExiste = await _tipoProductoService.ValidarByIdAsync(request.IdTipoProducto);
            if (!tipoProductoExiste)
                return Result.NotFound<int>("El tipo de producto no existe.");

            //5. Creamos el objeto, lo guardamos y lo retornamos
            var newModelo = new Modelo
            {
                Nombre = request.Nombre,
                IdMarca = request.IdMarca,
                IdTipoProducto = request.IdTipoProducto
            };

            await _modeloRepository.AddAsync(newModelo);
            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success(newModelo.Id);
        }

        public async Task<Result<Unit>> UpdateAsync(ModeloResponseDTO request)
        {
            //1. Validamos que haya cargado nombre
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return Result.BadRequest<Unit>("El nombre es obligatorio.");
            if (request.IdMarca <= 0)
                return Result.BadRequest<Unit>("La marca es obligatorio.");
            if (request.IdTipoProducto <= 0)
                return Result.BadRequest<Unit>("El tipo de producto es obligatorio.");

            //2. Validamos que el modelo exista
            var modelo = await _modeloRepository.GetByIdAsync(request.Id);
            if (modelo is null)
                return Result.NotFound<Unit>("El modelo no existe.");

            //3. Validamos que ya no este existente
            var id = await _modeloRepository.GetIdByNombreAsync(request.Nombre);
            if (id != 0 && id != request.Id)
                return Result.Conflict<Unit>("El modelo ya existe.");

            //4. Validamos que la marca exista
            var marcaExiste = await _marcaService.ValidarByIdAsync(request.IdMarca);
            if (!marcaExiste)
                return Result.NotFound<Unit>("La marca no existe.");

            //5. Validamos que el tipo de producto exista
            var tipoProductoExiste = await _tipoProductoService.ValidarByIdAsync(request.IdTipoProducto);
            if (!tipoProductoExiste)
                return Result.NotFound<Unit>("El tipo de producto no existe.");

            //6. Reemplazamos, guardamos y retornamos.
            modelo.Nombre = request.Nombre;
            modelo.IdMarca = request.IdMarca;
            modelo.IdTipoProducto = request.IdTipoProducto;
            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }
    }

}

