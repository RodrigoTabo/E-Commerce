using E_Commerce.Application.Interfaces.Atributos;
using E_Commerce.Application.Interfaces.AtributoValores;
using E_Commerce.Application.Interfaces.IUnitOfWorkRepository;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.AtributoValores;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class AtributoValorService
        (IAtributoValorRepository atributoValorRepository,
        IUnitOfWorkRepository unitOfWorkRepository,
        IAtributoService atributoService) : IAtributoValorService
    {
        private readonly IAtributoValorRepository _atributoValorRepository = atributoValorRepository;
        private readonly IAtributoService _atributoService = atributoService;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<Result<int>> CreateAsync(CreateAtributoValorRequestDTO request)
        {

            var atributo = await _atributoService.GetByIdAsync(request.IdAtributo);

            if (!atributo.Success)
                return Result.NotFound<int>("El atributo no existe.");

            var id = await _atributoValorRepository.GetIdByValorAsync(request.Valor);

            if (id != 0)
                return Result.Conflict<int>("El valor ya existe.");

            var valor = new AtributoValor
            {
                IdAtributo = request.IdAtributo,
                Valor = request.Valor
            };

            await _atributoValorRepository.AddAsync(valor);
            await _unitOfWorkRepository.SaveChangesAsync();

            return Result.Success(valor.Id);
        }

        public async Task<Result<List<ListAtributoValorResponseDTO>>> GetAllAsync()
        {
            var lista = await _atributoValorRepository.GetAllAsync();
            if (lista is null || !lista.Any())
                return Result.NotFound<List<ListAtributoValorResponseDTO>>("La lista no existe.");

            return Result.Success(lista);
        }

        public async Task<Result<Unit>> UpdateAsync(UpdateAtributoValorRequestDTO request)
        {
            var atributoValor = await _atributoValorRepository.GetByIdAsync(request.Id);

            if (atributoValor is null)
                return Result.NotFound<Unit>("El valor no existe");

            var atributo = await _atributoService.GetByIdAsync(request.IdAtributo);

            if (!atributo.Success)
                return Result.NotFound<Unit>("El atributo no existe.");

            //FALTA VALIDAR QUE NO INSERTE DATOS VACIOS!
            atributoValor.Valor = request.Valor;
            atributoValor.IdAtributo = request.IdAtributo;

            await _unitOfWorkRepository.SaveChangesAsync();
            return Result.Success();
        }
    }
}
