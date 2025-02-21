using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.GruposOpciones;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class GruposOpciones : System.Web.UI.Page
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
            Orden.Value = "0";  
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
        public static string ConsultaGrupos()
        {
            GruposOpcionesController _controller = new GruposOpcionesController();
            List<Models.GruposOpciones> _gruposOpciones = new List<Models.GruposOpciones>();

            _gruposOpciones = _controller.Consulta(0).Where(x => x.go_estado != "X").ToList();
            return JsonConvert.SerializeObject(_gruposOpciones);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarGrupo(string parametros)
        {
            GruposOpcionesController _controller = new GruposOpcionesController();
            Models.GruposOpciones parametro = new Models.GruposOpciones();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.go_codigo = Int16.Parse(arrayParametros[0].ToString());
            parametro.go_orden = Int16.Parse(arrayParametros[1].ToString());
            parametro.go_descripcion = arrayParametros[2].ToString();
            parametro.go_estado = arrayParametros[3].ToString();
            parametro.go_usuario_creacion  = user_cookie.Usuario;
            parametro.go_fecha_greacion = DateTime.Now;
            parametro.go_usuario_actualizacion  = user_cookie.Usuario;
            parametro.go_fecha_actualizacion = DateTime.Now;

            if (parametro.go_codigo == 0)
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarGrupo();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarGrupo();", true);
        }
    }
}