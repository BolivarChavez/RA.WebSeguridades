using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Empresas;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class Empresas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
            }
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string BuscarEmpresa()
        {
            EmpresasController _controller = new EmpresasController();
            List<Models.Empresas> _empresas = new List<Models.Empresas>();
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            _empresas = _controller.Consulta(int.Parse(idEmpresa)).ToList();
            return JsonConvert.SerializeObject(_empresas);
        }


        [System.Web.Services.WebMethod]
        public static string GrabarEmpresa(string parametros)
        {
            EmpresasController _controller = new EmpresasController();
            Models.Empresas parametro = new Models.Empresas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.em_codigo = Int16.Parse(arrayParametros[0].ToString());
            parametro.em_nombre = arrayParametros[1].ToString();
            parametro.em_pais = arrayParametros[2].ToString();
            parametro.em_provincia = arrayParametros[3].ToString();
            parametro.em_ciudad = arrayParametros[4].ToString();
            parametro.em_direccion = arrayParametros[5].ToString();
            parametro.em_telefono = arrayParametros[6].ToString();
            parametro.em_contacto = arrayParametros[7].ToString();
            parametro.em_email = arrayParametros[8].ToString();
            parametro.em_estado = "A";
            parametro.em_usuario_creacion = user_cookie.Usuario;
            parametro.em_fecha_creacion = DateTime.Now;
            parametro.em_usuario_actualizacion = user_cookie.Usuario;
            parametro.em_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);

            return response;
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarEmpresa();", true);
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "BuscarEmpresa();", true);
        }
    }
}