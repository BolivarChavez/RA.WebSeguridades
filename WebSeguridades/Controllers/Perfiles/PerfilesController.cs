using System.Collections.Generic;
using WebSeguridades.Interfaces.Perfiles;
using WebSeguridades.Services.Perfiles;

namespace WebSeguridades.Controllers.Perfiles
{
    public class PerfilesController : IPerfilesController
    {
        public string Actualizacion(Models.Perfiles perfil)
        {
            PerfilesService _perfilesService = new PerfilesService();

            return _perfilesService.Actualizacion(perfil);
        }

        public IEnumerable<Models.Perfiles> Consulta(int idPerfil)
        {
            PerfilesService _perfilesService = new PerfilesService();

            return _perfilesService.Consulta(idPerfil);
        }

        public string Ingreso(Models.Perfiles perfil)
        {
            PerfilesService _perfilesService = new PerfilesService();

            return _perfilesService.Ingreso(perfil);
        }
    }
}