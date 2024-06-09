using System.Collections.Generic;
using WebSeguridades.Interfaces.UsuarioPerfil;
using WebSeguridades.Services.UsuarioPerfil;

namespace WebSeguridades.Controllers
{
    public class UsuarioPerfilController : IUsuarioPerfilController
    {
        public string Actualizacion(Models.UsuarioPerfil usuario)
        {
            UsuarioPerfilService _usuarioService = new UsuarioPerfilService();

            return _usuarioService.Actualizacion(usuario);
        }

        public IEnumerable<Models.UsuarioPerfil> Consulta(int idUsuario)
        {
            UsuarioPerfilService _usuarioService = new UsuarioPerfilService();

            return _usuarioService.Consulta(idUsuario);
        }

        public string Eliminacion(Models.UsuarioPerfil usuario)
        {
            UsuarioPerfilService _usuarioService = new UsuarioPerfilService();

            return _usuarioService.Eliminacion(usuario);
        }

        public string Ingreso(Models.UsuarioPerfil usuario)
        {
            UsuarioPerfilService _usuarioService = new UsuarioPerfilService();

            return _usuarioService.Ingreso(usuario);
        }
    }
}