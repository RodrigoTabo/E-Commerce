namespace E_Commerce.Shared.DTOs.Reviews
{
    public record CrearReviewDTO
    {
        public int IdProducto { get; set; }
        public string Comentario { get; set; } = null!;
        public int Rating { get; set; }
    }
}
