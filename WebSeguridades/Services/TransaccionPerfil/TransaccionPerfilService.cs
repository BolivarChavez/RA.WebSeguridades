using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebSeguridades.Interfaces.TransaccionPerfil;

namespace WebSeguridades.Services.TransaccionPerfil
{
    public class TransaccionPerfilService : ITransaccionPerfilService
    {
        public string Actualizacion(Models.TransaccionPerfil perfil)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "TransaccionPerfil/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(perfil);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.TransaccionPerfil> Consulta(int idPerfil)
        {
            HttpClient client = new HttpClient();
            List<Models.TransaccionPerfil> listaError = new List<Models.TransaccionPerfil>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "TransaccionPerfil/Consulta/{0}", idPerfil.ToString().Trim()));

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
                listaError.Add(new Models.TransaccionPerfil
                {
                    tp_perfil = 0,
                    tp_transaccion = 0,
                    tp_grupo_opcion = 0,
                    tp_estado = errorContent,
                    tp_usuario_creacion = "",
                    tp_fecha_creacion = DateTime.Now,
                    tp_usuario_actualizacion = "",
                    tr_fecha_actualizacion = DateTime.Now
                });

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.TransaccionPerfil>>(respuesta);
            }
        }

        public string Eliminacion(Models.TransaccionPerfil perfil)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "TransaccionPerfil/Eliminacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(perfil);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public string Ingreso(Models.TransaccionPerfil perfil)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "TransaccionPerfil/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(perfil);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}