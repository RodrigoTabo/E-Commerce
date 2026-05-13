namespace E_Commerce.Domain.Entities
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<Pago> Pagos { get; set; } = new();
    }
}
