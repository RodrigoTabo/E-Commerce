using E_Commerce.Shared.DTOs.AtributoValores;
using ROP;

namespace E_Commerce.Application.Interfaces.AtributoValores
{
    public interface IAtributoValorService
    {
        Task<Result<List<ListAtributoValorResponseDTO>>> GetAllAsync();
        Task<Result<int>> CreateAsync(CreateAtributoValorRequestDTO request);
        Task<Result<Unit>> UpdateAsync(UpdateAtributoValorRequestDTO request);
        Task<bool> ValidarAtributosValor(List<int> idsAtributoValor);
    }
}
