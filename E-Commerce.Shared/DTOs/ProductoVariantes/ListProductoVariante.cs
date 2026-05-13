namespace E_Commerce.Shared.DTOs.ProductoVariantes
{
    public class ListProductoVariante
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string Producto { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public List<AtributoVarianteDTO> Atributos { get; set; } = [];

    }

}
