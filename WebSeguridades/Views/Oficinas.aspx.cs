using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Empresas;
using WebSeguridades.Controllers.Oficinas;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class Oficinas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaEmpresa();
                InitializedView();
            }
        }

        private void InitializedView()
        {
            Codigo.Value = "0";
            Nombre.Value = "";
            Pais.Value = "";
            Provincia.Value = "";
            Ciudad.Value = "";
            Direccion.Value = "";
            Telefono.Value = "";
            Contacto.Value = "";
            Email.Value = "";
            chkEstado.Checked = false;
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        private void CargaEmpresa()
        {
            EmpresasController _controller = new EmpresasController();
            List<Models.Empresas> _empresas = new List<Models.Empresas>();
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            _empresas = _controller.Consulta(int.Parse(idEmpresa)).ToList();
            Empresa.Value = _empresas.FirstOrDefault().em_codigo + " - " + _empresas.FirstOrDefault().em_nombre;
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaOficinas()
        {
            OficinasController _controller = new OficinasController();
            List<Models.Oficinas> _oficinas = new List<Models.Oficinas>();
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            _oficinas = _controller.Consulta(int.Parse(idEmpresa), 0).Where(x => x.of_estado != "X").ToList();
            return JsonConvert.SerializeObject(_oficinas);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarOficina(string parametros)
        {
            OficinasController _controller = new OficinasController();
            Models.Oficinas parametro = new Models.Oficinas();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.of_empresa = Int16.Parse(user_cookie.Empresa);
            parametro.of_codigo = Int16.Parse(arrayParametros[0].ToString());
            parametro.of_nombre = arrayParametros[1].ToString();
            parametro.of_pais = arrayParametros[2].ToString();
            parametro.of_provincia = arrayParametros[3].ToString();
            parametro.of_ciudad = arrayParametros[4].ToString();
            parametro.of_direccion = arrayParametros[5].ToString();
            parametro.of_telefono = arrayParametros[6].ToString();
            parametro.of_contacto = arrayParametros[7].ToString();
            parametro.of_email = arrayParametros[8].ToString();
            parametro.of_estado = arrayParametros[9].ToString();
            parametro.of_usuario_creacion = user_cookie.Usuario;
            parametro.of_fecha_creacion = DateTime.Now;
            parametro.of_usuario_actualizacion = user_cookie.Usuario;
            parametro.of_fecha_actualizacion = DateTime.Now;

            if (parametro.of_codigo == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            return response;
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGrid();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarOficina();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarOficina();", true);
        }
    }
}