namespace E_Commerce.Domain.Entities
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public Carrito Carrito { get; set; } = null!;
        public int IdCarrito { get; set; }
        public ProductoVariante ProductoVariante { get; set; } = null!;
        public int IdProductoVariante { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
