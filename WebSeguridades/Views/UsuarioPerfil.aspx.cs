using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Oficinas;
using WebSeguridades.Controllers.Perfiles;
using WebSeguridades.Controllers.UsuarioOficina;
using WebSeguridades.Controllers.Usuarios;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class UsuarioPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                InitializedView();
                CargaUsuarios();
                CargaPerfiles();
                Usuario.SelectedIndex = 0;
                Perfil.SelectedIndex = 0;
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
            Usuario.Enabled = true;
            Perfil.Enabled = true;
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

        private void CargaUsuarios()
        {
            UsuariosController _controller = new UsuariosController();
            List<Models.Usuarios> _usuarios = new List<Models.Usuarios>();

            _usuarios = _controller.Consulta(0, "*").Where(x => x.us_estado == "A").ToList();

            Usuario.DataSource = _usuarios;
            Usuario.DataValueField = "us_codigo";
            Usuario.DataTextField = "us_nombre";
            Usuario.DataBind();
        }

        private void CargaPerfiles()
        {
            PerfilesController _controller = new PerfilesController();
            List<Models.Perfiles> _perfiles = new List<Models.Perfiles>();

            _perfiles = _controller.Consulta(0).Where(x => x.pe_estado == "A").ToList();

            Perfil.DataSource = _perfiles;
            Perfil.DataValueField = "pe_codigo";
            Perfil.DataTextField = "pe_descripcion";
            Perfil.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPerfiles(string parametros)
        {
            UsuarioPerfilController _controllerUP = new UsuarioPerfilController();
            List<Models.UsuarioPerfil> _usuarioPerfil = new List<Models.UsuarioPerfil>();

            PerfilesController _controllerPerfiles = new PerfilesController();
            List<Models.Perfiles> _perfiles = new List<Models.Perfiles>();
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            _usuarioPerfil = _controllerUP.Consulta(int.Parse(parametros)).ToList();
            _perfiles = _controllerPerfiles.Consulta(0).Where(x => x.pe_estado == "A").ToList();

            var listaPerfiles = from usuarioPerfil in _usuarioPerfil
                                join perfil in _perfiles on usuarioPerfil.up_perfil equals perfil.pe_codigo
                                orderby perfil.pe_descripcion ascending
                                select new { usuarioPerfil.up_usuario, usuarioPerfil.up_perfil, perfil.pe_descripcion, usuarioPerfil.up_estado };

            return JsonConvert.SerializeObject(listaPerfiles);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarPerfil(string parametros)
        {
            UsuarioPerfilController _controller = new UsuarioPerfilController();
            Models.UsuarioPerfil parametro = new Models.UsuarioPerfil();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.up_perfil = Int16.Parse(arrayParametros[0].ToString());
            parametro.up_usuario = int.Parse(arrayParametros[1].ToString());
            parametro.up_estado = arrayParametros[2].ToString();
            parametro.up_usuario_creacion = user_cookie.Usuario;
            parametro.up_fecha_creacion = DateTime.Now;
            parametro.up_usuario_actualizacion = user_cookie.Usuario;
            parametro.up_fecha_actualizacion = DateTime.Now;

            if (arrayParametros[3].ToString().Trim() == "I")
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            return response;
        }

        [System.Web.Services.WebMethod]
        public static string EliminarPerfil(string parametros)
        {
            UsuarioPerfilController _controller = new UsuarioPerfilController();
            Models.UsuarioPerfil parametro = new Models.UsuarioPerfil();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.up_perfil = Int16.Parse(arrayParametros[0].ToString());
            parametro.up_usuario = int.Parse(arrayParametros[1].ToString());
            parametro.up_estado = arrayParametros[2].ToString();
            parametro.up_usuario_creacion = user_cookie.Usuario;
            parametro.up_fecha_creacion = DateTime.Now;
            parametro.up_usuario_actualizacion = user_cookie.Usuario;
            parametro.up_fecha_actualizacion = DateTime.Now;

            response = _controller.Eliminacion(parametro);
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