using System.Collections.Generic;
using WebSeguridades.Interfaces.Transacciones;
using WebSeguridades.Services.Transacciones;

namespace WebSeguridades.Controllers.Transacciones
{
    public class TransaccionesController : ITransaccionesController
    {
        public string Actualizacion(Models.Transacciones transaccion)
        {
            TransaccionesService _transaccionesService = new TransaccionesService();

            return _transaccionesService.Actualizacion(transaccion);
        }

        public IEnumerable<Models.Transacciones> Consulta(int idTransaccion)
        {
            TransaccionesService _transaccionesService = new TransaccionesService();

            return _transaccionesService.Consulta(idTransaccion);
        }

        public string Ingreso(Models.Transacciones transaccion)
        {
            TransaccionesService _transaccionesService = new TransaccionesService();

            return _transaccionesService.Ingreso(transaccion);
        }
    }
}