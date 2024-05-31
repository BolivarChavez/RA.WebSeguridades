using System.Collections.Generic;

namespace WebSeguridades.Interfaces.GruposOpciones
{
    public interface IGruposOpcionesController
    {
        string Ingreso(WebSeguridades.Models.GruposOpciones grupoOpcion);
        string Actualizacion(WebSeguridades.Models.GruposOpciones grupoOpcion);
        IEnumerable<WebSeguridades.Models.GruposOpciones> Consulta(int idGrupoOpcion);
    }
}
