using E_Commerce.Shared.DTOs.AtributoValores;

namespace E_Commerce.Shared.DTOs.ProductoVariantes
{
    public class ProductoVarianteDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public List<AtributoVarianteDTO> Atributos { get; set; } = [];
    }
}
