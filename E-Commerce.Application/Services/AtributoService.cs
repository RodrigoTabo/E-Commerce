using E_Commerce.Application.Interfaces.Atributos;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Atributos;
using ROP;

namespace E_Commerce.Application.Services
{
    public class AtributoService(IAtributoRepository atributoRepository, IUnitOfWorkRepository unitOfWorkRepository) : IAtributoService
    {
        private readonly IAtributoRepository _atributoRepository = atributoRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<int>> CreateAsync(CreateAtributoRequestDTO request)
        {

            var id = await _atributoRepository.GetIdByNombreAsync(request.Nombre);

            if (id != 0)
                return Result.Conflict<int>("El atributo ya existe.");

            var atributo = new Atributo
            {
                Nombre = request.Nombre
            };

            await _atributoRepository.AddAsync(atributo);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(atributo.Id);
        }

        public async Task<Result<List<AtributosResponseDTO>>> GetAllAsync()
        {
            var list = await _atributoRepository.GetAllAsync();

            if (list is null || !list.Any())
                return Result.NotFound<List<AtributosResponseDTO>>("La lista está vacía");

            return Result.Success(list);
        }


        public async Task<Result<Unit>> UpdateAsync(UpdateAtributoRequestDTO request)
        {
            var atributo = await _atributoRepository.GetById(request.Id);

            if (atributo is null)
                return Result.NotFound<Unit>("El atributo no existe");

            var id = await _atributoRepository.GetIdByNombreAsync(request.Nombre);

            if (id != 0)
                return Result.Conflict<Unit>("El atributo ya existe.");

            atributo.Nombre = request.Nombre;
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<int>> GetByIdAsync(int id)
        {
            var atributo = await _atributoRepository.GetById(id);

            if (atributo is null)
                return Result.NotFound<int>("El atributo no existe");

            return Result.Success(atributo.Id);
        }
    }
}
