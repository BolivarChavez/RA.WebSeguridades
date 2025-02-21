using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Transacciones;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class Transacciones : System.Web.UI.Page
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
            DescripcionLarga.Value = "";
            Programa.Value = "";
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
        public static string ConsultaTransacciones()
        {
            TransaccionesController _controller = new TransaccionesController();
            List<Models.Transacciones> _transacciones = new List<Models.Transacciones>();

            _transacciones = _controller.Consulta(0).Where(x => x.tr_estado != "X").ToList();
            return JsonConvert.SerializeObject(_transacciones);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarTransaccion(string parametros)
        {
            TransaccionesController _controller = new TransaccionesController();
            Models.Transacciones parametro = new Models.Transacciones();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.tr_codigo = int.Parse(arrayParametros[0].ToString());
            parametro.tr_descripcion  = arrayParametros[1].ToString().Trim();
            parametro.tr_descripcion_larga = arrayParametros[2].ToString().Trim();
            parametro.tr_tipo = arrayParametros[3].ToString().Trim();
            parametro.tr_programa = arrayParametros[4].ToString().Trim();
            parametro.tr_estado = arrayParametros[5].ToString().Trim();
            parametro.tr_usuario_ceacion = user_cookie.Usuario;
            parametro.tr_fecha_creacion = DateTime.Now;
            parametro.tr_usuario_actualizacion = user_cookie.Usuario;
            parametro.tr_fecha_actualizacion = DateTime.Now;

            if (parametro.tr_codigo == 0)
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarTransaccion();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarTransaccion();", true);
        }
    }
}