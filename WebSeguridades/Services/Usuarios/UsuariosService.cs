using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebSeguridades.Interfaces.Usuarios;

namespace WebSeguridades.Services.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        public string Actualizacion(Models.Usuarios usuario)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "Usuarios/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.Usuarios> Consulta(int codigoUsuario, string nombreUsuario)
        {
            HttpClient client = new HttpClient();
            List<Models.Usuarios> listaError = new List<Models.Usuarios>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Usuarios/Consulta/{0}/{1}", codigoUsuario.ToString().Trim(), nombreUsuario.Trim()));

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stoken);
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                respuesta = responseContent.ReadAsStringAsync().Result;
            }
            else
            {
                errorContent = response.StatusCode.ToString() + " - " + response.ReasonPhrase;
                respuesta = "";
            }

            if (respuesta == "")
            {
                listaError.Add(new Models.Usuarios
                {
                    us_codigo = 0,
                    us_nombre = errorContent,
                    us_login = "",
                    us_password = "",
                    us_email = "",
                    us_ingresos = 0,
                    us_ultimo_ingreso = DateTime.Now,
                    us_estado = "",
                    us_usuario_creacion = "",
                    us_fecha_creacion = DateTime.Now,
                    us_usuario_actualizacion = "",
                    us_fecha_actualizacion = DateTime.Now
                });

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Usuarios>>(respuesta);
            }
        }

        public string Ingreso(Models.Usuarios usuario)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "Usuarios/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}