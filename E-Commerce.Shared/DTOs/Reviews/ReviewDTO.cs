namespace E_Commerce.Shared.DTOs.Reviews
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string comentario { get; set; }
        public int rating { get; set; }
        public Guid? UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
