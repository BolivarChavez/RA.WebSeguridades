using System.Collections.Generic;

namespace WebSeguridades.Interfaces.Facultades
{
    public interface IFacultadesService
    {
        string Ingreso(WebSeguridades.Models.Facultades facultad);
        string Actualizacion(WebSeguridades.Models.Facultades facultad);
        IEnumerable<WebSeguridades.Models.Facultades> Consulta(int idFacultad);
    }
}
