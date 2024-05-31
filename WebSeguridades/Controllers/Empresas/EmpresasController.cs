using System.Collections.Generic;
using WebSeguridades.Interfaces.Empresas;
using WebSeguridades.Services.Empresas;

namespace WebSeguridades.Controllers.Empresas
{
    public class EmpresasController : IEmpresasController
    {
        public string Actualizacion(Models.Empresas empresa)
        {
            EmpresasService _empresaService = new EmpresasService();

            return _empresaService.Actualizacion(empresa);
        }

        public IEnumerable<Models.Empresas> Consulta(int idEmpresa)
        {
            EmpresasService _empresaService = new EmpresasService();

            return _empresaService.Consulta(idEmpresa);
        }

        public string Ingreso(Models.Empresas empresa)
        {
            EmpresasService _empresaService = new EmpresasService();

            return _empresaService.Ingreso(empresa);
        }
    }
}