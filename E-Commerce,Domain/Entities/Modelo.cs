namespace E_Commerce.Domain.Entities
{
    public class Modelo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public Marca Marca { get; set; } = null!;
        public int IdMarca { get; set; }
        public TipoProducto TipoProducto { get; set; } = null!;
        public int IdTipoProducto { get; set; }
        public List<Producto> Productos { get; set; } = new();
    }
}
