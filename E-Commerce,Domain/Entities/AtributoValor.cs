namespace E_Commerce.Domain.Entities
{
    public class AtributoValor
    {
        public int Id { get; set; }
        public Atributo Atributo { get; set; } = null!;
        public int IdAtributo { get; set; }
        public string Valor { get; set; } = null!;
        public List<ProductoAtributoVariante> ProductoAtributoVariantes { get; set; }
    }
}
