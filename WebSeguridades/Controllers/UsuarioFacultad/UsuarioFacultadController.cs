using System.Collections.Generic;
using WebSeguridades.Interfaces.UsuarioFacultad;
using WebSeguridades.Services.UsuarioFacultad;

namespace WebSeguridades.Controllers.UsuarioFacultad
{
    public class UsuarioFacultadController : IUsuarioFacultadController
    {
        public string Actualizacion(Models.UsuarioFacultad usuario)
        {
            UsuarioFacultadService _usuarioService = new UsuarioFacultadService();

            return _usuarioService.Actualizacion(usuario);
        }

        public IEnumerable<Models.UsuarioFacultad> Consulta(int idUsuario, int idTransaccion)
        {
            UsuarioFacultadService _usuarioService = new UsuarioFacultadService();

            return _usuarioService.Consulta(idUsuario, idTransaccion);
        }

        public string Eliminacion(Models.UsuarioFacultad usuario)
        {
            UsuarioFacultadService _usuarioService = new UsuarioFacultadService();

            return _usuarioService.Eliminacion(usuario);
        }

        public string Ingreso(Models.UsuarioFacultad usuario)
        {
            UsuarioFacultadService _usuarioService = new UsuarioFacultadService();

            return _usuarioService.Ingreso(usuario);
        }
    }
}