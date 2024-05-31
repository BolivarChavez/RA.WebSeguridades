using System.Collections.Generic;
using WebSeguridades.Interfaces.Facultades;
using WebSeguridades.Services.Facultades;

namespace WebSeguridades.Controllers.Facultades
{
    public class FacultadesController : IFacultadesController
    {
        public string Actualizacion(Models.Facultades facultad)
        {
            FacultadesService _facultadesService = new FacultadesService();

            return _facultadesService.Actualizacion(facultad);
        }

        public IEnumerable<Models.Facultades> Consulta(int idFacultad)
        {
            FacultadesService _facultadesService = new FacultadesService();

            return _facultadesService.Consulta(idFacultad);
        }

        public string Ingreso(Models.Facultades facultad)
        {
            FacultadesService _facultadesService = new FacultadesService();

            return _facultadesService.Ingreso(facultad);
        }
    }
}