using System.Collections.Generic;

namespace WebSeguridades.Interfaces.TransaccionFacultad
{
    public interface ITransaccionFacultadService
    {
        string Ingreso(WebSeguridades.Models.TransaccionFacultad transaccion);
        string Actualizacion(WebSeguridades.Models.TransaccionFacultad transaccion);
        string Eliminacion(WebSeguridades.Models.TransaccionFacultad transaccion);
        IEnumerable<WebSeguridades.Models.TransaccionFacultad> Consulta(int idTransaccion);
    }
}
