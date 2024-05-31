using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebSeguridades.Interfaces.Perfiles;

namespace WebSeguridades.Services.Perfiles
{
    public class PerfilesService : IPerfilesService
    {
        public string Actualizacion(Models.Perfiles perfil)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Perfiles/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(perfil);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.Perfiles> Consulta(int idPerfil)
        {
            HttpClient client = new HttpClient();
            List<Models.Perfiles> listaError = new List<Models.Perfiles>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Perfiles/Consulta/{0}", idPerfil.ToString().Trim()));

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
                listaError.Add(new Models.Perfiles
                {
                    pe_codigo = 0,
                    pe_descripcion = errorContent,
                    pe_estado = "",
                    pe_usuario_creacion = "",
                    pe_fecha_creacion = DateTime.Now,
                    pe_usuario_actualizacion = "",
                    pe_fecha_actualizacion = DateTime.Now
                });

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Perfiles>>(respuesta);
            }
        }

        public string Ingreso(Models.Perfiles perfil)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Perfiles/Ingreso";
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