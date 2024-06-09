using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebSeguridades.Interfaces.Empresas;

namespace WebSeguridades.Services.Empresas
{
    public class EmpresasService : IEmpresasService
    {
        public string Actualizacion(Models.Empresas empresa)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "Empresas/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(empresa);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.Empresas> Consulta(int idEmpresa)
        {
            HttpClient client = new HttpClient();
            List<Models.Empresas> listaError = new List<Models.Empresas>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Empresas/Consulta/{0}", idEmpresa.ToString().Trim()));

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
                listaError.Add(new Models.Empresas {
                        em_codigo = 0,
                        em_nombre = errorContent,
                        em_pais = "",
                        em_provincia = "",
                        em_ciudad = "",
                        em_direccion = "",
                        em_telefono = "",
                        em_contacto = "",
                        em_email = "",
                        em_estado = "",
                        em_usuario_creacion = "",
                        em_fecha_creacion = DateTime.Now,
                        em_usuario_actualizacion = "",
                        em_fecha_actualizacion = DateTime.Now});

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Empresas>>(respuesta);
            }
        }

        public string Ingreso(Models.Empresas empresa)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "Empresas/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(empresa);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}