using E_Commerce.Application.Interfaces;
using E_Commerce.Application.Interfaces.Auth;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Application.Interfaces.User;
using E_Commerce.Shared.DTOs.User;
using ROP;

namespace E_Commerce.Application.Services
{
    public class UserService(IUserRepository userRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        ICurrentUserService currentUserService,
        IImagenStorageService imagenStorageService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IImagenStorageService _imagenStorageService = imagenStorageService;

        public async Task<Result<Unit>> ActualizarPerfil(UserRequestDTO request)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.BadRequest<Unit>("Tienes que estar conectado para esta acción.");

            var validarDatosUser = ValidarDatosUser(request);
            if (!validarDatosUser.Success)
                return Result.Failure<Unit>(validarDatosUser.Errors);

            var usuario = await _userRepository.GetByUserId(userId);
            if (usuario is null)
                return Result.NotFound<Unit>("El usuario no existe.");

            usuario.Nombre = request.Nombre;
            usuario.Apellido = request.Apellido;
            usuario.DNI = request.DNI;

            if (request.ImagenPerfilForm is not null)
            {
                if (!string.IsNullOrWhiteSpace(usuario.UrlImagen))
                {
                    await _imagenStorageService.DeleteAsync(usuario.UrlImagen);
                }
                
                var path = await _imagenStorageService.SaveAsync(
                    request.ImagenPerfilForm.OpenReadStream(),
                    request.ImagenPerfilForm.ContentType,
                    $"users/{usuario.Id}");

                usuario.UrlImagen = path;
            }

            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<UserRequestDTO>> GetByUserId()
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
                return Result.BadRequest<UserRequestDTO>("Tienes que estar conectado para esta acción");

            var GetByUserId = await _userRepository.GetByUserId(userId);
            if (GetByUserId is null)
                return Result.NotFound<UserRequestDTO>("El usuario no existe.");

            var dto = new UserRequestDTO
            {
                Nombre = GetByUserId.Nombre,
                Apellido = GetByUserId.Apellido,
                DNI = GetByUserId.DNI,
                Email = GetByUserId.Email,
                UrlImagen = GetByUserId.UrlImagen,
                CreateAt = GetByUserId.CreatedAt
            };

            return Result.Success(dto);
        }


        //Metodos privados

        private Result<Unit> ValidarDatosUser(UserRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return Result.BadRequest<Unit>("El nombre no puede estar vacio.");
            if (string.IsNullOrWhiteSpace(request.Apellido))
                return Result.BadRequest<Unit>("El apellido no puede estar vacio.");
            if (string.IsNullOrWhiteSpace(request.DNI))
                return Result.BadRequest<Unit>("El DNI no puede estar vacio.");
            return Result.Success();
        }

    }
}
