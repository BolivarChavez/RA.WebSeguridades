using System.Collections.Generic;

namespace WebSeguridades.Interfaces.TransaccionPerfil
{
    public interface ITransaccionPerfilController
    {
        string Ingreso(WebSeguridades.Models.TransaccionPerfil perfil);
        string Actualizacion(WebSeguridades.Models.TransaccionPerfil perfil);
        string Eliminacion(WebSeguridades.Models.TransaccionPerfil perfil);
        IEnumerable<WebSeguridades.Models.TransaccionPerfil> Consulta(int idPerfil);
    }
}
