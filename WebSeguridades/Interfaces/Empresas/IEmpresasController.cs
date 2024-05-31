using System.Collections.Generic;

namespace WebSeguridades.Interfaces.Empresas
{
    public interface IEmpresasController
    {
        string Ingreso(WebSeguridades.Models.Empresas empresa);
        string Actualizacion(WebSeguridades.Models.Empresas empresa);
        IEnumerable<WebSeguridades.Models.Empresas> Consulta(int idEmpresa);
    }
}
