using System.Collections.Generic;
using WebSeguridades.Interfaces.TransaccionPerfil;
using WebSeguridades.Services.TransaccionPerfil;

namespace WebSeguridades.Controllers.TransaccionPerfil
{
    public class TransaccionPerfilController : ITransaccionPerfilController
    {
        public string Actualizacion(Models.TransaccionPerfil perfil)
        {
            TransaccionPerfilService _perfilService = new TransaccionPerfilService();

            return _perfilService.Actualizacion(perfil);
        }

        public IEnumerable<Models.TransaccionPerfil> Consulta(int idPerfil)
        {
            TransaccionPerfilService _perfilService = new TransaccionPerfilService();

            return _perfilService.Consulta(idPerfil);
        }

        public string Eliminacion(Models.TransaccionPerfil perfil)
        {
            TransaccionPerfilService _perfilService = new TransaccionPerfilService();

            return _perfilService.Eliminacion(perfil);
        }

        public string Ingreso(Models.TransaccionPerfil perfil)
        {
            TransaccionPerfilService _perfilService = new TransaccionPerfilService();

            return _perfilService.Ingreso(perfil);
        }
    }
}