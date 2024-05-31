using System.Collections.Generic;

namespace WebSeguridades.Interfaces.UsuarioFacultad
{
    public interface IUsuarioFacultadService
    {
        string Ingreso(WebSeguridades.Models.UsuarioFacultad usuario);
        string Actualizacion(WebSeguridades.Models.UsuarioFacultad usuario);
        string Eliminacion(WebSeguridades.Models.UsuarioFacultad usuario);
        IEnumerable<WebSeguridades.Models.UsuarioFacultad> Consulta(int idUsuario, int idTransaccion);
    }
}
