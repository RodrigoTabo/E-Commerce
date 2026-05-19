using E_Commerce.Shared.DTOs.Ciudades;
using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Ciudades
{
    public interface ICiudadService
    {
        Task<Result<int>> GetLocalidadById(int id);
        Task<Result<List<CiudadesDTO>>> GetAllAsync();
    }
}
