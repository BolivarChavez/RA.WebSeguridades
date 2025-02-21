using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Facultades;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class Facultades : System.Web.UI.Page
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

            if (user_cookie.Usuario == null || user_cookie.Usuario.Trim() == "")
            {
                Response.Redirect("ErrorAccesoOpcion.aspx", true);
            }

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaFacultades()
        {
            FacultadesController _controller = new FacultadesController();
            List<Models.Facultades> _facultades = new List<Models.Facultades>();

            _facultades = _controller.Consulta(0).Where(x => x.fa_codigo > 0 && x.fa_estado != "X").ToList();
            return JsonConvert.SerializeObject(_facultades);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarFacultad(string parametros)
        {
            FacultadesController _controller = new FacultadesController();
            Models.Facultades parametro = new Models.Facultades();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.fa_codigo = Int16.Parse(arrayParametros[0].ToString());
            parametro.fa_descripcion = arrayParametros[1].ToString().Trim().ToUpper();
            parametro.fa_estado = arrayParametros[2].ToString();
            parametro.fa_usuario_creacion = user_cookie.Usuario;
            parametro.fa_fecha_creacion = DateTime.Now;
            parametro.fa_usuario_actualizacion = user_cookie.Usuario;
            parametro.fa_fecha_actualizacion = DateTime.Now;

            if (parametro.fa_codigo == 0)
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarFacultad();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarFacultad();", true);
        }
    }
}