namespace E_Commerce.Shared.DTOs.ProductoVariantes
{
    public class UpdateProductoVarianteDTO
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public List<int> IdsAtributoValor { get; set; } = new();
    }
}
