namespace E_Commerce.Shared.DTOs.Carritos
{
    public class CarritoItemDto
    {
        public int IdProductoVariante { get; set; }
        public string? ProductoNombre { get; set; }
        public int ProductoStock { get; set; }
        public string? ProductoUrlImagen { get; set; }
        public string? ModeloNombre { get; set; }
        public decimal CarritoPrecio { get; set; }
        public int CarritoCantidad { get; set; }
    }
}
