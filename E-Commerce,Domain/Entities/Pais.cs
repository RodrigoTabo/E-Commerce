namespace E_Commerce.Domain.Entities
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<Provincia> Provincias { get; set; } = new();
    }
}
