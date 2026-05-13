using E_Commerce.Shared.DTOs.Orden;
using E_Commerce.Shared.DTOs.Pagos;

namespace E_Commerce.Shared.DTOs.OrdenCreate
{
    public class CrearOrdenConPagoRequest
    {
        public CreateOrdenRequest Orden { get; set; } = null!;
        public PagoRequestDTO Pago { get; set; } = null!;
    }
}
