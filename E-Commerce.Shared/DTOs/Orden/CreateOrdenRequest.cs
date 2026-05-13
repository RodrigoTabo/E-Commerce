namespace E_Commerce.Shared.DTOs.Orden
{
    public record CreateOrdenRequest
    {
        public int? IdDomicilio { get; set; }
        public int IdMetodoEnvio { get; set; }
        public decimal Total { get; set; }
        public string CalleSnapshot { get; set; } = string.Empty;
        public int AlturaSnapshot { get; set; }
        public string CiudadSnapshot { get; set; } = string.Empty;
        public string CodigoPostaSnapshot { get; set; } = string.Empty;
    }

}
