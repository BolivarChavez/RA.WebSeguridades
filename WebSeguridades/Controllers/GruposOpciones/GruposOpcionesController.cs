using System.Collections.Generic;
using WebSeguridades.Interfaces.GruposOpciones;
using WebSeguridades.Services.GruposOpciones;

namespace WebSeguridades.Controllers.GruposOpciones
{
    public class GruposOpcionesController : IGruposOpcionesController
    {
        public string Actualizacion(Models.GruposOpciones grupoOpcion)
        {
            GruposOpcionesService _gruposOpcionesService = new GruposOpcionesService();

            return _gruposOpcionesService.Actualizacion(grupoOpcion);
        }

        public IEnumerable<Models.GruposOpciones> Consulta(int idGrupoOpcion)
        {
            GruposOpcionesService _gruposOpcionesService = new GruposOpcionesService();

            return _gruposOpcionesService.Consulta(idGrupoOpcion);
        }

        public string Ingreso(Models.GruposOpciones grupoOpcion)
        {
            GruposOpcionesService _gruposOpcionesService = new GruposOpcionesService();

            return _gruposOpcionesService.Ingreso(grupoOpcion);
        }
    }
}