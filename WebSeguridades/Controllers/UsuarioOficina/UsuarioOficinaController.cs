using System.Collections.Generic;
using WebSeguridades.Interfaces.UsuarioOficina;
using WebSeguridades.Services.UsuarioOficina;

namespace WebSeguridades.Controllers.UsuarioOficina
{
    public class UsuarioOficinaController : IUsuarioOficinaController
    {
        public string Actualizacion(Models.UsuarioOficina usuario)
        {
            UsuarioOficinaService _usuarioService = new UsuarioOficinaService();

            return _usuarioService.Actualizacion(usuario);
        }

        public IEnumerable<Models.UsuarioOficina> Consulta(int idUsuario, int idEmpresa)
        {
            UsuarioOficinaService _usuarioService = new UsuarioOficinaService();

            return _usuarioService.Consulta(idUsuario, idEmpresa);
        }

        public string Eliminacion(Models.UsuarioOficina usuario)
        {
            UsuarioOficinaService _usuarioService = new UsuarioOficinaService();

            return _usuarioService.Eliminacion(usuario);
        }

        public string Ingreso(Models.UsuarioOficina usuario)
        {
            UsuarioOficinaService _usuarioService = new UsuarioOficinaService();

            return _usuarioService.Ingreso(usuario);
        }
    }
}