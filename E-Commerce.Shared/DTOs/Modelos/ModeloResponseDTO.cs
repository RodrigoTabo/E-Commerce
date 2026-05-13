namespace E_Commerce.Shared.DTOs.Modelos
{
    public record ModeloResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdMarca { get; set; }
        public string Marca { get; set; }
        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }
    }
}
