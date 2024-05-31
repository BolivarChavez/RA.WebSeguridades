using System.Collections.Generic;

namespace WebSeguridades.Interfaces.Transacciones
{
    public interface ITransaccionesService
    {
        string Ingreso(WebSeguridades.Models.Transacciones transaccion);
        string Actualizacion(WebSeguridades.Models.Transacciones transaccion);
        IEnumerable<WebSeguridades.Models.Transacciones> Consulta(int idTransaccion);
    }
}
