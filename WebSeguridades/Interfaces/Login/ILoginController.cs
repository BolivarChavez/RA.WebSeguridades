using WebSeguridades.Models;

namespace WebSeguridades.Interfaces.Login
{
    public interface ILoginController
    {
        string ProcesaLogin(LoginUsuario login);
    }
}
