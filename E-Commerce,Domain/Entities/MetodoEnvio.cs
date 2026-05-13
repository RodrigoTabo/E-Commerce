namespace E_Commerce.Domain.Entities
{
    public class MetodoEnvio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<Orden> Ordenes { get; set; } = new();
        public List<OrdenItem> OrdenItems { get; set; } = new();
    }
}
