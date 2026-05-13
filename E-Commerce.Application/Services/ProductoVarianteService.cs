using E_Commerce.Application.Interfaces.AtributoValores;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.ProductoVariantes;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.ProductoVariantes;
using ROP;

namespace E_Commerce.Application.Services
{
    public class ProductoVarianteService(IProductoVarianteRepository productoVarianteRepository,
        IAtributoValorService atributoValorService,
        IUnitOfWorkRepository unitOfWorkRepository) : IProductoVarianteService
    {

        private readonly IProductoVarianteRepository _productoVarianteRepository = productoVarianteRepository;
        private readonly IAtributoValorService _atributoValorService = atributoValorService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<int>> CreateAsync(CreateProductoVarianteDTO request)
        {
            //1.Validamos propiedades de la request. 
            var validarRequest = ValidarRequest(request.IdProducto, request.IdsAtributoValor, request.Precio, request.Stock);
            //Si estan mal cargados, retornamos BADREQUEST.
            if (!validarRequest.Success)
                return Result.Failure<int>(validarRequest.Errors);
            //2. Validamos si los atributosValorIds seleccionados, existen en la DB
            var validarCombinacionesExistentes = await _atributoValorService.ValidarAtributosValor(request.IdsAtributoValor);
            //Si no existen retornamos NOTFOUND.
            if (!validarCombinacionesExistentes)
                return Result.NotFound<int>("Los atributos seleccionados no existen.");

            //3.Validar si la variante ya existe con el mismo atributovalor.
            var validarCombinacionesExistente = await _productoVarianteRepository.GetIdProductoVarianteByCombinaciones(request.IdProducto, request.IdsAtributoValor);
            //Si retorna valor, existe.
            if (validarCombinacionesExistente != 0)
                return Result.Conflict<int>("La variante del producto ya existe.");

            //Creamos y guardamos.
            var nuevaVariante = new ProductoVariante
            {
                IdProducto = request.IdProducto,
                Precio = request.Precio,
                Stock = request.Stock,
                ProductoAtributoVariantes = request.IdsAtributoValor
                .Select(id => new ProductoAtributoVariante
                {
                    IdAtributoValor = id,
                    IdProductoVariante = 0 
                })
                .ToList()
            };
            await _productoVarianteRepository.AddAsync(nuevaVariante);
            await _unitOfWorkRepository.SaveChangesAsync();
            return nuevaVariante.Id;
        }

        public async Task<Result<ProductoVarianteDTO?>> GetByIdAsync(int Id)
        {
            var getById = await _productoVarianteRepository.GetByIdAsync(Id);
            if (getById is null)
                return Result.NotFound<ProductoVarianteDTO?>("La variante no existe");

            return Result.Success(getById);
        }

        public async Task<Result<ProductoVariante>> GetProductoVarianteById(int Id)
        {
            var existeProductoVariante = await _productoVarianteRepository.GetProductoVarianteById(Id);
            if (existeProductoVariante is null)
                return Result.NotFound<ProductoVariante>("La variante del producto no existe.");

            return Result.Success(existeProductoVariante);
        }

        public async Task<Result<List<ListProductoVariante>>> GetProductoVarianteByIdProducto(int IdProducto)
        {
            var listProductoVariante = await _productoVarianteRepository.GetProductoVarianteByIdProducto(IdProducto);
            if (listProductoVariante is null)
                return Result.Conflict<List<ListProductoVariante>>("El producto no tiene ninguna variante.");

            return Result.Success(listProductoVariante);
        }

        public async Task<Result<int>> GetStockById(int Id)
        {
            var stock = await _productoVarianteRepository.GetStockById(Id);
            if (stock <= 0)
                return Result.Conflict<int>("La variante del producto no tiene stock");
            return Result.Success(stock);
        }

        public async Task<Result<List<ProductoVariante>>> ListaProductosVariantesByIds(List<int> IdsProductosVariantes)
        {
            var productoVarianteList = await _productoVarianteRepository.ListaProductosVariantesByIds(IdsProductosVariantes);

            if (!productoVarianteList.Any())
                return Result.Conflict<List<ProductoVariante>>("Ha ocurrido un problema con la lista de las varientes de productos");

            return Result.Success(productoVarianteList);

        }

        public async Task<Result<Unit>> UpdateAsync(UpdateProductoVarianteDTO request)
        {
            var productoVariante = await _productoVarianteRepository.GetProductoVarianteById(request.Id);

            if (productoVariante is null)
                return Result.NotFound<Unit>("Producto variante no encontrada.");

            var validarRequest = ValidarRequest(
                request.IdProducto,
                request.IdsAtributoValor,
                request.Precio,
                request.Stock);

            if (!validarRequest.Success)
                return Result.Failure<Unit>(validarRequest.Errors);

            var validarCombinacionesExistentes =
                await _atributoValorService.ValidarAtributosValor(request.IdsAtributoValor);

            if (!validarCombinacionesExistentes)
                return Result.NotFound<Unit>("Los atributos seleccionados no existen.");

            var validarCombinacionesExistente =
                await _productoVarianteRepository
                    .GetIdProductoVarianteByCombinaciones(request.IdProducto, request.IdsAtributoValor);

            if (validarCombinacionesExistente != 0 &&
                validarCombinacionesExistente != request.Id)
                return Result.Conflict<Unit>("La variante del producto ya existe.");

            productoVariante.Stock = request.Stock;
            productoVariante.Precio = request.Precio;

            await _productoVarianteRepository.RemoveByVarianteIdAsync(productoVariante.Id);

            foreach (var id in request.IdsAtributoValor)
            {
                productoVariante.ProductoAtributoVariantes.Add(new ProductoAtributoVariante
                {
                    IdAtributoValor = id,
                    IdProductoVariante = productoVariante.Id
                });
            }

            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success();
        }

        private Result<Unit> ValidarRequest(int IdProducto, List<int> IdsAtributoValor, decimal Precio, int Stock)
        {
            if (IdProducto <= 0)
                return Result.BadRequest<Unit>("Debes cargar el producto");

            foreach (var item in IdsAtributoValor)
            {
                if (item <= 0)
                    return Result.BadRequest<Unit>("Debes cargar el atributo");
            }
            if (Precio <= 0)
                return Result.BadRequest<Unit>("Debes cargar un precio");
            if (Stock <= 0)
                return Result.BadRequest<Unit>("Debes cargar un stock");

            return Result.Success();
        }
    }
}
