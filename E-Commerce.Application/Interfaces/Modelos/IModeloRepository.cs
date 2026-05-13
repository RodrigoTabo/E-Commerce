using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOs.Modelos;

namespace E_Commerce.Application.Interfaces.Modelos
{
    public interface IModeloRepository
    {
        Task<Modelo?> GetByIdAsync(int? id);
        Task<List<ModeloDetalleDTO>> GetAllMoMaProAsync();

        //Cruds
        Task AddAsync(Modelo modelo);
        Task<List<ModeloResponseDTO>> GetAllAsync();
        Task<int> GetIdByNombreAsync(string nombre);
        Task<Modelo?> GetByIdAsync(int id);
    }
}
