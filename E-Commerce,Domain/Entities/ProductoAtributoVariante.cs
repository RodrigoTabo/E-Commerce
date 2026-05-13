namespace E_Commerce.Domain.Entities
{
    public class ProductoAtributoVariante
    {
        public ProductoVariante ProductoVariante { get; set; } = null!;
        public int IdProductoVariante { get; set; }
        public AtributoValor AtributoValor { get; set; } = null!;
        public int IdAtributoValor { get; set; }
    }
}
