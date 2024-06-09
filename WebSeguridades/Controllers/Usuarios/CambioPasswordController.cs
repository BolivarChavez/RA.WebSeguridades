using System;
using System.Linq;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Interfaces.Usuarios;
using WebSeguridades.Models;
using WebSeguridades.Services.Usuarios;
using WebSeguridades.Services.Utils;

namespace WebSeguridades.Controllers.Usuarios
{
    public class CambioPasswordController : ICambioPasswordController
    {
        public string CambioPassword(int idUsuario, string password)
        {
            UsuariosService _usuarioService = new UsuariosService();
            CifradoService _cifrado = new CifradoService();
            Models.Usuarios usuario = new Models.Usuarios();
            Models.Usuarios parametro = new Models.Usuarios();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            usuario = _usuarioService.Consulta(idUsuario, "*").FirstOrDefault();

            parametro.us_codigo = usuario.us_codigo;
            parametro.us_nombre = usuario.us_nombre;
            parametro.us_login = usuario.us_login;
            parametro.us_password = _cifrado.Encriptar(password.ToLower());
            parametro.us_email = usuario.us_email;
            parametro.us_ingresos = 0;
            parametro.us_ultimo_ingreso = DateTime.Now;
            parametro.us_estado = usuario.us_estado;
            parametro.us_usuario_creacion = user_cookie.Usuario;
            parametro.us_fecha_creacion = DateTime.Now;
            parametro.us_usuario_actualizacion = user_cookie.Usuario;
            parametro.us_fecha_actualizacion = DateTime.Now;
            
            return _usuarioService.Actualizacion(parametro);
        }
    }
}