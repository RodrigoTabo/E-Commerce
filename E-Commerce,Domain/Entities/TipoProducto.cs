namespace E_Commerce.Domain.Entities
{
    public class TipoProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<Modelo> Modelos { get; set; } = new();
    }
}
