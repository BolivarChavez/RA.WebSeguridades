using System.Collections.Generic;

namespace WebSeguridades.Interfaces.Usuarios
{
    public interface IUsuariosController
    {
        string Ingreso(WebSeguridades.Models.Usuarios usuario);
        string Actualizacion(WebSeguridades.Models.Usuarios usuario);
        IEnumerable<WebSeguridades.Models.Usuarios> Consulta(int codigoUsuario, string nombreUsuario);
    }
}
