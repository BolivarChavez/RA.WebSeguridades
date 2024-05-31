using WebSeguridades.Models;

namespace WebSeguridades.Interfaces.Login
{
    public interface ILoginService
    {
        Retorno LoginUsuario(LoginUsuario login);

        string ValidaLogin(LoginUsuario login);
    }
}
