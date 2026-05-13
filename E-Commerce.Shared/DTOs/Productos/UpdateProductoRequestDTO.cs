namespace E_Commerce.Shared.DTOs.Productos
{
    public class UpdateProductoRequestDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? UrlImagen { get; set; }
        public string? Descripcion { get; set; }
        public int? IdModelo { get; set; }
    }
}
