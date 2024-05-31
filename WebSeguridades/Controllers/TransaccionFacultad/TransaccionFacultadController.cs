using System.Collections.Generic;
using WebSeguridades.Interfaces.TransaccionFacultad;
using WebSeguridades.Services.TransaccionFacultad;

namespace WebSeguridades.Controllers.TransaccionFacultad
{
    public class TransaccionFacultadController : ITransaccionFacultadController
    {
        public string Actualizacion(Models.TransaccionFacultad transaccion)
        {
            TransaccionFacultadService _transaccionService = new TransaccionFacultadService();

            return _transaccionService.Actualizacion(transaccion);
        }

        public IEnumerable<Models.TransaccionFacultad> Consulta(int idTransaccion)
        {
            TransaccionFacultadService _transaccionService = new TransaccionFacultadService();

            return _transaccionService.Consulta(idTransaccion);
        }

        public string Eliminacion(Models.TransaccionFacultad transaccion)
        {
            TransaccionFacultadService _transaccionService = new TransaccionFacultadService();

            return _transaccionService.Eliminacion(transaccion);
        }

        public string Ingreso(Models.TransaccionFacultad transaccion)
        {
            TransaccionFacultadService _transaccionService = new TransaccionFacultadService();

            return _transaccionService.Ingreso(transaccion);
        }
    }
}