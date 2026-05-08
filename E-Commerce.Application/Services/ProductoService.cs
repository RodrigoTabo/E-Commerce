using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.Modelos;
using E_Commerce.Application.Interfaces.Productos;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Productos;
using ROP;
using System.Collections.Immutable;

namespace E_Commerce.Application.Services
{
    public class ProductoService
        (IProductoRepository productoRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        IModeloService modeloService,
        ICurrentUserService currentUserService) : IProductoService
    {
        private readonly IProductoRepository _productoRepository = productoRepository;
        private readonly IModeloService _modeloService = modeloService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<List<ProductoResponseDTO>>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();

            // Usamos tu validador (Convertimos IEnumerable a List para que coincida con tu firma)
            var validation = ValidateProductos(productos?.ToList());
            if (!validation.Success)
                return Result.Failure<List<ProductoResponseDTO>>(validation.Errors);

            // Mapeo manual
            var dtos = new List<ProductoResponseDTO>();
            foreach (var p in validation.Value)
            {
                dtos.Add(MapToDto(p));
            }

            return Result.Success(dtos);
        }

        public async Task<Result<int>> CreateAsync(CreateProductoRequestDTO request)
        {
            // 1. Validar request
            var validation = ValidateProductoRequest(request);
            if (!validation.Success)
                return Result.Failure<int>(validation.Errors);

            var dto = validation.Value;

            // 2. Validar modelo
            var modeloResult = await ValidateModelo(dto);
            if (!modeloResult.Success)
                return Result.Failure<int>(modeloResult.Errors);

            // 3. Usuario actual
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.Conflict<int>("Usuario no autenticado");

            // 4. Crear entidad
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                //Stock = dto.Stock,
                //Precio = dto.Precio,
                UrlImagen = dto.UrlImagen,
                Descripcion = dto.Descripcion,
                IdModelo = dto.IdModelo,
                IdApplicationUser = userId.Value
            };

            await _productoRepository.AddAsync(producto);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(producto.Id);
        }

        public async Task<Result<ProductoResponseDTO>> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);

            if (producto == null)
                return Result.NotFound<ProductoResponseDTO>("No hay productos");

            // Usamos tu validador de DTO
            return ValidateProductoById(producto);
        }

        public async Task<Result<Unit>> UpdateAsync(UpdateProductoRequestDTO request)
        {
            var producto = await _productoRepository.GetProductoByIdAsync(request.Id);
            if (producto is null)
                return Result.NotFound<Unit>("El producto no existe.");

            var modelo = await _modeloService.GetModeloByIdAsync(request.IdModelo);
            if (!modelo.Success)
                return Result.NotFound<Unit>("El modelo no existe.");

            producto.Nombre = request.Nombre;
            producto.Descripcion = request.Descripcion;
            producto.UrlImagen = request.UrlImagen;
            producto.IdModelo = request.IdModelo;

            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Producto>>> ListaProductosByIds(List<int> IdsProductos)
        {
            var productoList = await _productoRepository.ListaProductosByIds(IdsProductos);

            if (!productoList.Any())
                return Result.Conflict<List<Producto>>("Ha ocurrido un problema con la lista de productos");

            return Result.Success(productoList);

        }

        public Result<Unit> DescontarStock(List<Producto> productos, List<CarritoItem> carritoItems)
        {
            var productosDict = productos
                .ToDictionary(p => p.Id);

            foreach (var item in carritoItems)
            {
                if (!productosDict.TryGetValue(item.IdProducto, out var producto))
                    return Result.Failure<Unit>($"Producto {item.IdProducto} no existe.");

                //if (producto.Stock < item.Cantidad)
                //    return Result.Failure<Unit>($"El producto {producto.Nombre} no tiene stock");
            }

            foreach (var item in carritoItems)
            {
                var producto = productosDict[item.IdProducto];
                //producto.Stock -= item.Cantidad;
            }

            return Result.Success();
        }


        private Result<List<Producto>> ValidateProductos(List<Producto>? productos)
        {
            if (productos is null || !productos.Any())
            {
                return Result.NotFound<List<Producto>>("No hay productos");
            }

            return Result.Success(productos);
        }

        private Result<ProductoResponseDTO> ValidateProductoById(ProductoResponseDTO dto)
        {
            if (dto is null)
            {
                return Result.NotFound<ProductoResponseDTO>("No hay productos");
            }

            return Result.Success(dto);
        }

        private async Task<Result<CreateProductoRequestDTO>> ValidateModelo(CreateProductoRequestDTO dto)
        {
            //var modeloResult = await _modeloService.GetByIdAsync(dto.IdModelo);

            //if (!modeloResult.Success)
            //    return Result.NotFound<CreateProductoRequestDTO>("El Modelo no existe.");

            return Result.Success(dto);
        }

        private ProductoResponseDTO MapToDto(Producto p)
        {
            return new ProductoResponseDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                //Precio = p.Precio,
                //Stock = p.Stock,
                UrlImagen = p.UrlImagen ?? "Imagen no cargada.",
                MarcaNombre = p.Modelo?.Marca?.Nombre ?? "Sin Marca",
                CategoriaNombre = p.Modelo?.TipoProducto?.Nombre ?? "Sin Tipo",
                Modelo = p.Modelo?.Nombre ?? "Sin Modelo",
                IdModelo = p.IdModelo,
                NombreVendedor = p.ApplicationUser?.Nombre ?? "Sin Usuario",
                CreateAt = p.CreatedAt
            };
        }

        private Result<CreateProductoRequestDTO> ValidateProductoRequest(CreateProductoRequestDTO dto)
        {
            List<Error> errores = new();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errores.Add(Error.Create("Añade un Nombre."));
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                errores.Add(Error.Create("Añade una Descripcion."));
            if (string.IsNullOrWhiteSpace(dto.UrlImagen))
                errores.Add(Error.Create("Añade una Imagen."));
            //if (dto.Precio <= 0)
            //    errores.Add(Error.Create("Añade el Precio."));
            //if (dto.Stock <= 0)
            //    errores.Add(Error.Create("Añade el Stock."));
            if (dto.IdModelo <= 0)
                errores.Add(Error.Create("Añade el Modelo."));

            if (errores.Any())
                return Result.BadRequest<CreateProductoRequestDTO>(errores.ToImmutableArray());

            return Result.Success(dto);
        }
    }
}