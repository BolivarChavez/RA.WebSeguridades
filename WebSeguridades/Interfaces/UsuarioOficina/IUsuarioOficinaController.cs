using System.Collections.Generic;

namespace WebSeguridades.Interfaces.UsuarioOficina
{
    public interface IUsuarioOficinaController
    {
        string Ingreso(WebSeguridades.Models.UsuarioOficina usuario);
        string Actualizacion(WebSeguridades.Models.UsuarioOficina usuario);
        string Eliminacion(WebSeguridades.Models.UsuarioOficina usuario);
        IEnumerable<WebSeguridades.Models.UsuarioOficina> Consulta(int idUsuario, int idEmpresa);
    }
}
