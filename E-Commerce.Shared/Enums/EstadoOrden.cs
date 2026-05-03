using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.Enums
{
    public enum EstadoOrden
    {
        Creada,
        PendientePago,
        Pagada,
        Preparando,
        Enviada,
        Entregada,
        Cancelada,
        Expirada,
        Reembolsada
    }
}
