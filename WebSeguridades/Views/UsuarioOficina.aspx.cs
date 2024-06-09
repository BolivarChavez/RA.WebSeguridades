using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Oficinas;
using WebSeguridades.Controllers.UsuarioOficina;
using WebSeguridades.Controllers.Usuarios;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class UsuarioOficina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaUsuarios();
                CargaOficinas();
                InitializedView();

                Usuario.SelectedIndex = 0;
                Oficina.SelectedIndex = 0;  
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
            Usuario.Enabled = true;
            Oficina.Enabled = true;
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

        private void CargaOficinas()
        {
            OficinasController _controller = new OficinasController();
            List<Models.Oficinas> _oficinas = new List<Models.Oficinas>();
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            _oficinas = _controller.Consulta(int.Parse(idEmpresa), 0).Where(x => x.of_estado == "A").ToList();

            Oficina.DataSource = _oficinas;
            Oficina.DataValueField = "of_codigo";
            Oficina.DataTextField = "of_nombre";
            Oficina.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaOficinas(string parametros)
        {
            UsuarioOficinaController _controllerUO = new UsuarioOficinaController();
            List<Models.UsuarioOficina> _usuarioOficina = new List<Models.UsuarioOficina>();

            OficinasController _controllerOficina = new OficinasController();
            List<Models.Oficinas> _oficinas = new List<Models.Oficinas>();
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            _usuarioOficina = _controllerUO.Consulta(int.Parse(parametros), int.Parse(idEmpresa)).ToList();
            _oficinas = _controllerOficina.Consulta(int.Parse(idEmpresa), 0).Where(x => x.of_estado == "A").ToList();

            var listaOficinas = from usuarioOficina in _usuarioOficina
                                join oficina in _oficinas on usuarioOficina.uo_oficina equals oficina.of_codigo
                                orderby oficina.of_nombre ascending
                                select new { usuarioOficina.uo_usuario, usuarioOficina.uo_oficina, oficina.of_nombre, usuarioOficina.uo_estado };

            return JsonConvert.SerializeObject(listaOficinas);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarOficina(string parametros)
        {
            UsuarioOficinaController _controller = new UsuarioOficinaController();
            Models.UsuarioOficina parametro = new Models.UsuarioOficina();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            parametro.uo_usuario = int.Parse(arrayParametros[0].ToString());
            parametro.uo_empresa = Int16.Parse(idEmpresa);
            parametro.uo_oficina = Int16.Parse(arrayParametros[1].ToString());
            parametro.uo_estado = arrayParametros[2].ToString();
            parametro.uo_usuario_creacion = user_cookie.Usuario;
            parametro.uo_fecha_creacion = DateTime.Now;
            parametro.uo_usuario_actualizacion = user_cookie.Usuario;
            parametro.uo_fecha_actualizacion = DateTime.Now;

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
        public static string EliminarOficina(string parametros)
        {
            UsuarioOficinaController _controller = new UsuarioOficinaController();
            Models.UsuarioOficina parametro = new Models.UsuarioOficina();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');
            string idEmpresa = ConfigurationManager.AppSettings["CodigEmpresa"].ToString();

            parametro.uo_usuario = int.Parse(arrayParametros[0].ToString());
            parametro.uo_empresa = Int16.Parse(idEmpresa);
            parametro.uo_oficina = Int16.Parse(arrayParametros[1].ToString());
            parametro.uo_estado = arrayParametros[2].ToString();
            parametro.uo_usuario_creacion = user_cookie.Usuario;
            parametro.uo_fecha_creacion = DateTime.Now;
            parametro.uo_usuario_actualizacion = user_cookie.Usuario;
            parametro.uo_fecha_actualizacion = DateTime.Now;

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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarOficina();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarOficina();", true);
        }
    }
}