using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Usuarios;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class Usuarios : System.Web.UI.Page
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
            Nombre.Value = "";
            Login.Value = "";
            Password.Value = "";
            Email.Value = "";
            HiddenField1.Value = "";
            Password.Disabled = false;
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
        public static string ConsultaUsuarios()
        {
            UsuariosController _controller = new UsuariosController();
            List<Models.Usuarios> _usuarios = new List<Models.Usuarios>();

            _usuarios = _controller.Consulta(0, "*").Where(x => x.us_estado != "X").ToList();
            return JsonConvert.SerializeObject(_usuarios);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarUsuario(string parametros)
        {
            UsuariosController _controller = new UsuariosController();
            Models.Usuarios parametro = new Models.Usuarios();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.us_codigo = Int16.Parse(arrayParametros[0].ToString());
            parametro.us_nombre = arrayParametros[1].ToString().Trim().ToUpper();
            parametro.us_login = arrayParametros[2].ToString().Trim();
            parametro.us_password = arrayParametros[3].ToString().Trim();
            parametro.us_email = arrayParametros[4].ToString().Trim();
            parametro.us_ingresos = 0;
            parametro.us_ultimo_ingreso = DateTime.Now;
            parametro.us_estado = arrayParametros[5].ToString();
            parametro.us_usuario_creacion = user_cookie.Usuario;
            parametro.us_fecha_creacion = DateTime.Now;
            parametro.us_usuario_actualizacion = user_cookie.Usuario;
            parametro.us_fecha_actualizacion = DateTime.Now;

            if (parametro.us_codigo == 0)
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
            InitializedView();
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGrid();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarUsuario();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarUsuario();", true);
        }
    }
}