using System.Collections.Generic;

namespace WebSeguridades.Interfaces.UsuarioPerfil
{
    public interface IUsuarioPerfilService
    {
        string Ingreso(WebSeguridades.Models.UsuarioPerfil usuario);
        string Actualizacion(WebSeguridades.Models.UsuarioPerfil usuario);
        string Eliminacion(WebSeguridades.Models.UsuarioPerfil usuario);
        IEnumerable<WebSeguridades.Models.UsuarioPerfil> Consulta(int idUsuario);
    }
}
