using System.Collections.Generic;
using WebSeguridades.Interfaces.Oficinas;
using WebSeguridades.Services.Oficinas;

namespace WebSeguridades.Controllers.Oficinas
{
    public class OficinasController : IOficinasController
    {
        public string Actualizacion(Models.Oficinas oficina)
        {
            OficinasService _oficinasService = new OficinasService();

            return _oficinasService.Actualizacion(oficina);
        }

        public IEnumerable<Models.Oficinas> Consulta(int idEmpresa, int idOficina)
        {
            OficinasService _oficinasService = new OficinasService();

            return _oficinasService.Consulta(idEmpresa, idOficina);
        }

        public string Ingreso(Models.Oficinas oficina)
        {
            OficinasService _oficinasService = new OficinasService();

            return _oficinasService.Ingreso(oficina);
        }
    }
}