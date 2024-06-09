using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Facultades;
using WebSeguridades.Controllers.Perfiles;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class Perfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                InitializedView();
            }
        }

        private void InitializedView()
        {
            Codigo.Value = "0";
            Descripcion.Value = "";
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

        [System.Web.Services.WebMethod]
        public static string ConsultaPerfiles()
        {
            PerfilesController _controller = new PerfilesController();
            List<Models.Perfiles> _perfiles = new List<Models.Perfiles>();

            _perfiles = _controller.Consulta(0).Where(x => x.pe_estado != "X").ToList();
            return JsonConvert.SerializeObject(_perfiles);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarPerfil(string parametros)
        {
            PerfilesController _controller = new PerfilesController();
            Models.Perfiles parametro = new Models.Perfiles();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.pe_codigo = Int16.Parse(arrayParametros[0].ToString());
            parametro.pe_descripcion = arrayParametros[1].ToString().Trim().ToUpper();
            parametro.pe_estado = arrayParametros[2].ToString();
            parametro.pe_usuario_creacion = user_cookie.Usuario;
            parametro.pe_fecha_creacion = DateTime.Now;
            parametro.pe_usuario_actualizacion = user_cookie.Usuario;
            parametro.pe_fecha_actualizacion = DateTime.Now;

            if (parametro.pe_codigo == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            return response;
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGrid();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarPerfil();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarPerfil();", true);
        }
    }
}