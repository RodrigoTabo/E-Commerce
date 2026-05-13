namespace E_Commerce.Shared.DTOs.Atributos
{
    public record UpdateAtributoRequestDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
