using E_Commerce.Shared.DTOs.MetodoEnvios;

namespace E_Commerce.Application.Interfaces.MetodoEnvios
{
    public interface IMetodoEnvioRepository
    {
        Task<int> MetodoEnvioExistente(int IdIdMetodoEnvio);
        Task<List<MetodoEnvioDTO>> GetAllAsync();
    }
}
