using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.Pagos;
using ROP;

namespace E_Commerce.Application.Interfaces.Ordenes
{
    public interface IOrdenService
    {
        Task<Result<int>> CreateAsync(CreateOrdenRequest request, PagoRequestDTO pagorequest);
        Task<Result<List<ListOrdenDTO>>> GetOrdenesAsync();
        Task<Result<OrdenDetailsDTO>> GetOrdenById(int id);
        Task<Result<List<OrdenDetailsUserDTO>>> ListOrdenByUsers();
        Task<Result<Unit>> AprobarPagoAsync(int ordenId);
        Task<Result<Unit>> PrepararOrdenAsync(int ordenId);
        Task<Result<Unit>> EnviarOrdenAsync(int ordenId);
        Task<Result<Unit>> EntregarOrdenAsync(int ordenId);
        Task<Result<Unit>> CargarComprobante(ComprobanteUploadDTO request);

    }
}
