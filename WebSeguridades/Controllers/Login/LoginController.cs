using System;
using System.Linq;
using System.Web;
using WebSeguridades.Interfaces.Login;
using WebSeguridades.Models;
using WebSeguridades.Services.Login;
using WebSeguridades.Services.Usuarios;

namespace WebSeguridades.Controllers.Login
{
    public class LoginController : ILoginController
    {
        public string ProcesaLogin(LoginUsuario login)
        {
            LoginService _LoginService = new LoginService();
            UsuariosService _OperadorService = new UsuariosService();
            string respvalida;

            respvalida = _LoginService.ValidaLogin(login);

            if (respvalida == "")
            {
                var retorno = _LoginService.LoginUsuario(login);

                if (retorno.retorno == 0)
                {
                    var operador = _OperadorService.Consulta(0, login.usuario);

                    if (HttpContext.Current.Request.Cookies["userInfo"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("userInfo");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                        HttpContext.Current.Response.Cookies.Add(myCookie);
                    }

                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["Usuario"] = operador.ToList().FirstOrDefault().us_login.Trim();
                    userInfo["Nombre"] = operador.ToList().FirstOrDefault().us_nombre.Trim(); 
                    userInfo["Token"] = retorno.descripcion;
                    userInfo["CodigoUsuario"] = operador.ToList().FirstOrDefault().us_codigo.ToString().Trim();
                    userInfo.Expires.Add(new TimeSpan(8, 0, 0));
                    HttpContext.Current.Response.Cookies.Add(userInfo);

                    return "1|success|Bienvenido!|Sus credenciales se validaron correctamente|Consola.aspx|200";
                }
                else
                {
                    return "0|error|Error en credenciales de ingreso|" + retorno.mensaje + "|Inicio.aspx|401";
                }
            }
            else
            {
                return "0|error|Ingreso no correcto|" + respvalida + "|Inicio.aspx|401";
            }
        }
    }
}