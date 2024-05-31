using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebSeguridades.Interfaces.UsuarioPerfil;

namespace WebSeguridades.Services.UsuarioPerfil
{
    public class UsuarioPerfilService : IUsuarioPerfilService
    {
        public string Actualizacion(Models.UsuarioPerfil usuario)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "UsuarioPerfil/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.UsuarioPerfil> Consulta(int idUsuario)
        {
            HttpClient client = new HttpClient();
            List<Models.UsuarioPerfil> listaError = new List<Models.UsuarioPerfil>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "UsuarioPerfil/Consulta/{0}", idUsuario.ToString().Trim()));

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
                listaError.Add(new Models.UsuarioPerfil
                {
                    up_perfil = 0,
                    up_usuario = 0,
                    up_estado = errorContent,
                    up_usuario_creacion = "",
                    up_fecha_creacion = DateTime.Now,
                    up_usuario_actualizacion = "",
                    up_fecha_actualizacion = DateTime.Now
                });

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.UsuarioPerfil>>(respuesta);
            }
        }

        public string Eliminacion(Models.UsuarioPerfil usuario)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "UsuarioPerfil/Eliminacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(usuario);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public string Ingreso(Models.UsuarioPerfil usuario)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "UsuarioPerfil/Ingreso";
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