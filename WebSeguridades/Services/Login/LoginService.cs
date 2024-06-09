using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using WebSeguridades.Interfaces.Login;
using WebSeguridades.Models;

namespace WebSeguridades.Services.Login
{
    public class LoginService : ILoginService
    {
        public Retorno LoginUsuario(LoginUsuario login)
        {
            string url = string.Empty;
            string _key = string.Empty;
            LoginUsuario NewLogin = new LoginUsuario();

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "Usuarios/ProcesaLogin";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            NewLogin.usuario = login.usuario;
            NewLogin.password = login.password;

            var json = JsonConvert.SerializeObject(NewLogin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<Retorno>>(result).FirstOrDefault();
        }

        public string ValidaLogin(LoginUsuario login)
        {
            string usuarioAdministrador = ConfigurationManager.AppSettings["UserAdmin"].ToString().Trim();
            string respuesta = string.Empty;

            if (login.usuario.Trim().Length <= 1)
            {
                respuesta = "Se debe ingresar un nombre de usuario";
            }

            if (login.password.Trim().Length <= 1)
            {
                respuesta = "Se debe ingresar una contraseña";
            }

            if (!login.usuario.Trim().All(char.IsLetter))
            {
                respuesta = "El nombre de usuario contiene caracteres no validos";
            }

            if (usuarioAdministrador != login.usuario.Trim())
            {
                respuesta = "El nombre de usuario del administrador no corresponde al que esta configurado en la aplicacion";
            }

            return respuesta;
        }
    }
}