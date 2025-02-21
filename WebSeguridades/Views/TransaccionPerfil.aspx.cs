using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.GruposOpciones;
using WebSeguridades.Controllers.Perfiles;
using WebSeguridades.Controllers.Transacciones;
using WebSeguridades.Controllers.TransaccionPerfil;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class TransaccionPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaPerfiles();
                CargaTransacciones();
                CargaGruposOpciones();
                InitializedView();
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
            Transaccion.Enabled = true;
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

        private void CargaTransacciones()
        {
            TransaccionesController _controller = new TransaccionesController();
            List<Models.Transacciones> _transacciones = new List<Models.Transacciones>();

            _transacciones = _controller.Consulta(0).Where(x => x.tr_estado == "A").ToList();

            Transaccion.DataSource = _transacciones;
            Transaccion.DataValueField = "tr_codigo";
            Transaccion.DataTextField = "tr_descripcion";
            Transaccion.DataBind();
        }

        private void CargaGruposOpciones()
        {
            GruposOpcionesController _controller = new GruposOpcionesController();
            List<Models.GruposOpciones> _grupos = new List<Models.GruposOpciones>();

            _grupos = _controller.Consulta(0).Where(x => x.go_estado == "A").ToList();

            Grupo.DataSource = _grupos;
            Grupo.DataValueField = "go_codigo";
            Grupo.DataTextField = "go_descripcion";
            Grupo.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaTransacciones(string parametros)
        {
            TransaccionPerfilController _controller = new TransaccionPerfilController();
            List<Models.TransaccionPerfil> _transaccionPerfiles = new List<Models.TransaccionPerfil>();

            TransaccionesController _controllerTransaccion = new TransaccionesController();
            List<Models.Transacciones> _transacciones = new List<Models.Transacciones>();

            GruposOpcionesController _controllerGrupoOpcion = new GruposOpcionesController();
            List<Models.GruposOpciones> _grupos = new List<Models.GruposOpciones>();

            _transaccionPerfiles = _controller.Consulta(int.Parse(parametros)).ToList();
            _transacciones = _controllerTransaccion.Consulta(0).Where(x => x.tr_estado == "A").ToList();
            _grupos = _controllerGrupoOpcion.Consulta(0).Where(x => x.go_estado == "A").ToList();

            var listaTransacciones = from perfilTransaccion in _transaccionPerfiles
                                     join transaccion in _transacciones on perfilTransaccion.tp_transaccion equals transaccion.tr_codigo
                                     join grupo in _grupos on perfilTransaccion.tp_grupo_opcion equals grupo.go_codigo
                                     orderby grupo.go_descripcion, perfilTransaccion.tp_transaccion ascending
                                  select new { perfilTransaccion.tp_perfil, perfilTransaccion.tp_transaccion, transaccion.tr_descripcion, perfilTransaccion.tp_grupo_opcion, grupo.go_descripcion, perfilTransaccion.tp_estado };

            return JsonConvert.SerializeObject(listaTransacciones);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarTransaccion(string parametros)
        {
            TransaccionPerfilController _controller = new TransaccionPerfilController();
            Models.TransaccionPerfil parametro = new Models.TransaccionPerfil();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.tp_perfil = Int16.Parse(arrayParametros[0].ToString());
            parametro.tp_transaccion = int.Parse(arrayParametros[1].ToString());
            parametro.tp_grupo_opcion = Int16.Parse(arrayParametros[2].ToString());
            parametro.tp_estado = arrayParametros[3].ToString();
            parametro.tp_usuario_creacion = user_cookie.Usuario;
            parametro.tp_fecha_creacion = DateTime.Now;
            parametro.tp_usuario_actualizacion = user_cookie.Usuario;
            parametro.tr_fecha_actualizacion = DateTime.Now;

            if (arrayParametros[4].ToString().Trim() == "I")
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
        public static string EliminarTransaccion(string parametros)
        {
            TransaccionPerfilController _controller = new TransaccionPerfilController();
            Models.TransaccionPerfil parametro = new Models.TransaccionPerfil();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.tp_perfil = Int16.Parse(arrayParametros[0].ToString());
            parametro.tp_transaccion = int.Parse(arrayParametros[1].ToString());
            parametro.tp_grupo_opcion = Int16.Parse(arrayParametros[2].ToString());
            parametro.tp_estado = arrayParametros[3].ToString();
            parametro.tp_usuario_creacion = user_cookie.Usuario;
            parametro.tp_fecha_creacion = DateTime.Now;
            parametro.tp_usuario_actualizacion = user_cookie.Usuario;
            parametro.tr_fecha_actualizacion = DateTime.Now;

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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarTransaccion();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarTransaccion();", true);
        }
    }
}