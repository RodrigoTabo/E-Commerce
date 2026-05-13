using ROP;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Ciudades
{
    public interface ICiudadService
    {
        Task<Result<int>> GetLocalidadById(int id);
    }
}
