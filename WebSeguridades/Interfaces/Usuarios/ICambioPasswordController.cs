namespace WebSeguridades.Interfaces.Usuarios
{
    public interface ICambioPasswordController
    {
        string CambioPassword(int idUsuario, string password);
    }
}
