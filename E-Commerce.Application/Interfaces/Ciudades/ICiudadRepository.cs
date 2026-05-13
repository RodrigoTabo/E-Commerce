using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Interfaces.Ciudades
{
    public interface ICiudadRepository
    {
        Task<int> GetLocalidadById(int id);
    }
}
