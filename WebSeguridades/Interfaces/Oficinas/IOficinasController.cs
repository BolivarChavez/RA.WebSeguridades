using System.Collections.Generic;

namespace WebSeguridades.Interfaces.Oficinas
{
    public interface IOficinasController
    {
        string Ingreso(WebSeguridades.Models.Oficinas oficina);
        string Actualizacion(WebSeguridades.Models.Oficinas oficina);
        IEnumerable<WebSeguridades.Models.Oficinas> Consulta(int idEmpresa, int idOficina);
    }
}
