namespace E_Commerce.Shared.DTOs.Pagos
{
    public record PagoRequestDTO
    {
        public int IdMetodoPago { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
