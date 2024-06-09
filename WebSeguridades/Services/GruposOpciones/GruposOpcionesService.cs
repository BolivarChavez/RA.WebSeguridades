using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebSeguridades.Interfaces.GruposOpciones;

namespace WebSeguridades.Services.GruposOpciones
{
    public class GruposOpcionesService : IGruposOpcionesService
    {
        public string Actualizacion(Models.GruposOpciones grupoOpcion)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "GruposOpciones/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(grupoOpcion);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.GruposOpciones> Consulta(int idGrupoOpcion)
        {
            HttpClient client = new HttpClient();
            List<Models.GruposOpciones> listaError = new List<Models.GruposOpciones>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "GruposOpciones/Consulta/{0}", idGrupoOpcion.ToString().Trim()));

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
                listaError.Add(new Models.GruposOpciones
                {
                    go_codigo = 0,
                    go_orden = 0,
                    go_descripcion = errorContent,
                    go_estado = "",
                    go_usuario_creacion = "",
                    go_fecha_greacion = DateTime.Now,
                    go_usuario_actualizacion = "",
                    go_fecha_actualizacion = DateTime.Now
                });

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.GruposOpciones>>(respuesta);
            }
        }

        public string Ingreso(Models.GruposOpciones grupoOpcion)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "GruposOpciones/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(grupoOpcion);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}