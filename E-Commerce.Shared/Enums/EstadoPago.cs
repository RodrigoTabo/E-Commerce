using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.Enums
{
    public enum EstadoPago
    {
        Pendiente,        // se inició el pago pero no está confirmado
        Procesando,       // gateway bancario / MP lo está validando
        Aprobado,         // pago confirmado OK
        Rechazado,        // tarjeta/medio falló
        Cancelado,        // usuario canceló antes de completar
        Reembolsado,      // dinero devuelto
        Expirado          // tiempo de pago vencido
    }
}
