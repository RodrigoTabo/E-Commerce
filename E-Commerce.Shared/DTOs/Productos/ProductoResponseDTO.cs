using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Productos
{
    public record ProductoResponseDTO(
        int Id,
        string Nombre,
        string Descripcion,
        decimal Precio, 
        int Stock,     
        string UrlImagen,
        string MarcaNombre,     
        string CategoriaNombre,
        string Modelo,
        string NombreVendedor,
        DateTime Publicado
    );
}
