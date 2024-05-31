using System.Collections.Generic;

namespace WebSeguridades.Interfaces.Perfiles
{
    public interface IPerfilesController
    {
        string Ingreso(WebSeguridades.Models.Perfiles perfil);
        string Actualizacion(WebSeguridades.Models.Perfiles perfil);
        IEnumerable<WebSeguridades.Models.Perfiles> Consulta(int idPerfil);
    }
}
