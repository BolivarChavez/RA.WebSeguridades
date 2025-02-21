using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Facultades;
using WebSeguridades.Controllers.Transacciones;
using WebSeguridades.Controllers.TransaccionFacultad;
using WebSeguridades.Controllers.TransaccionPerfil;
using WebSeguridades.Controllers.UsuarioFacultad;
using WebSeguridades.Controllers.Usuarios;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class UsuarioFacultad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaUsuarios();
                InitializedView();
                Usuario.SelectedIndex = 0;
                CargaTransacciones(int.Parse(Usuario.SelectedValue));
                Transaccion.SelectedIndex = 0;
                CargaFacultades(int.Parse(Transaccion.SelectedValue));
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
            Usuario.Enabled = true;
            Transaccion.Enabled = true;
            Facultad.Enabled = true;
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

        private void CargaTransacciones(int idUsuario)
        {
            UsuarioPerfilController _controllerUp = new UsuarioPerfilController();
            List<Models.UsuarioPerfil> _usuarioPerfil = new List<Models.UsuarioPerfil>();

            TransaccionPerfilController _controllerTp = new TransaccionPerfilController();
            List<Models.TransaccionPerfil> _transaccionPerfil = new List<Models.TransaccionPerfil>();

            TransaccionesController _controllerTran = new TransaccionesController();
            List<Models.Transacciones> _transaccion = new List<Models.Transacciones>();

            _usuarioPerfil = _controllerUp.Consulta(idUsuario).Where(x => x.up_estado == "A").ToList();
            _transaccionPerfil = _controllerTp.Consulta(0).Where(x => x.tp_estado == "A").ToList();
            _transaccion = _controllerTran.Consulta(0).Where(x => x.tr_estado == "A").ToList();

            var listaTransacciones = from transaccion in _transaccion
                                  join transaccionPerfil in _transaccionPerfil on transaccion.tr_codigo equals transaccionPerfil.tp_transaccion
                                  join usuarioPerfil in _usuarioPerfil on transaccionPerfil.tp_perfil equals usuarioPerfil.up_perfil
                                  orderby transaccion.tr_descripcion ascending
                                  select new { transaccion.tr_codigo, transaccion.tr_descripcion };

            var listaTransaccionesUsuario = listaTransacciones.Distinct().ToList();

            Transaccion.DataSource = listaTransaccionesUsuario;
            Transaccion.DataValueField = "tr_codigo";
            Transaccion.DataTextField = "tr_descripcion";
            Transaccion.DataBind();
        }

        private void CargaFacultades(int idTransaccion)
        {
            TransaccionFacultadController _controllerTF = new TransaccionFacultadController();
            List<Models.TransaccionFacultad> _transaccionFacultad = new List<Models.TransaccionFacultad>();

            FacultadesController _controller = new FacultadesController();
            List<Models.Facultades> _facultades = new List<Models.Facultades>();

            _transaccionFacultad = _controllerTF.Consulta(idTransaccion).Where(x => x.tf_estado == "A").ToList();
            _facultades = _controller.Consulta(0).Where(x => x.fa_estado == "A").ToList();

            var listaFacultades = from facultad in _facultades
                                  join transaccionFacultad in _transaccionFacultad on facultad.fa_codigo equals transaccionFacultad.tf_facultad
                                  orderby facultad.fa_descripcion
                                  select new { facultad.fa_codigo, facultad.fa_descripcion };

            Facultad.DataSource = listaFacultades;
            Facultad.DataValueField = "fa_codigo";
            Facultad.DataTextField = "fa_descripcion";
            Facultad.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaFacultades(string parametros)
        {
            UsuarioFacultadController _controller = new UsuarioFacultadController();
            List<Models.UsuarioFacultad> _usuarioFacultades = new List<Models.UsuarioFacultad>();

            FacultadesController _controllerFacultad = new FacultadesController();
            List<Models.Facultades> _facultades = new List<Models.Facultades>();

            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _usuarioFacultades = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1])).ToList();
            _facultades = _controllerFacultad.Consulta(0).Where(x => x.fa_estado == "A").ToList();

            var listaFacultades = from usuarioFacultad in _usuarioFacultades
                                  join facultad in _facultades on usuarioFacultad.uf_facultad equals facultad.fa_codigo
                                  orderby facultad.fa_descripcion ascending
                                  select new { usuarioFacultad.uf_usuario, usuarioFacultad.uf_transaccion, usuarioFacultad.uf_facultad, facultad.fa_descripcion, usuarioFacultad.uf_estado };

            return JsonConvert.SerializeObject(listaFacultades);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarFacultad(string parametros)
        {
            UsuarioFacultadController _controller = new UsuarioFacultadController();
            Models.UsuarioFacultad parametro = new Models.UsuarioFacultad();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.uf_usuario = int.Parse(arrayParametros[0].ToString());
            parametro.uf_transaccion = int.Parse(arrayParametros[1].ToString());
            parametro.uf_facultad = Int16.Parse(arrayParametros[2].ToString());
            parametro.uf_estado = arrayParametros[3].ToString();
            parametro.uf_usuario_creacion = user_cookie.Usuario;
            parametro.uf_fecha_creacion = DateTime.Now;
            parametro.uf_usuario_actualizacion = user_cookie.Usuario;
            parametro.uf_fecha_actualizacion = DateTime.Now;

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
        public static string EliminarFacultad(string parametros)
        {
            UsuarioFacultadController _controller = new UsuarioFacultadController();
            Models.UsuarioFacultad parametro = new Models.UsuarioFacultad();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.uf_usuario = int.Parse(arrayParametros[0].ToString());
            parametro.uf_transaccion = int.Parse(arrayParametros[1].ToString());
            parametro.uf_facultad = Int16.Parse(arrayParametros[2].ToString());
            parametro.uf_estado = arrayParametros[3].ToString();
            parametro.uf_usuario_creacion = user_cookie.Usuario;
            parametro.uf_fecha_creacion = DateTime.Now;
            parametro.uf_usuario_actualizacion = user_cookie.Usuario;
            parametro.uf_fecha_actualizacion = DateTime.Now;

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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarFacultad();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarFacultad();", true);
        }

        protected void Usuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaTransacciones(int.Parse(Usuario.SelectedValue));
        }

        protected void Transaccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaFacultades(int.Parse(Transaccion.SelectedValue));
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGrid();", true);
        }
    }
}