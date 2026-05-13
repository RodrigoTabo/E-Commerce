namespace E_Commerce.Shared.DTOs.AtributoValores
{
    public class AtributoValorDTO
    {
        public int Id { get; set; }
        public string Atributo { get; set; } = null!;
        public int IdAtributo { get; set; }
        public string Valor { get; set; } = null!;
    }
}
