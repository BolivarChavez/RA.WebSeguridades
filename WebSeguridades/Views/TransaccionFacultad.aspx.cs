using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Controllers.Facultades;
using WebSeguridades.Controllers.Transacciones;
using WebSeguridades.Controllers.TransaccionFacultad;
using WebSeguridades.Models;

namespace WebSeguridades.Views
{
    public partial class TransaccionFacultad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaFacultades();
                CargaTransacciones();
                InitializedView();
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
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

        private void CargaFacultades()
        {
            FacultadesController _controller = new FacultadesController();
            List<Models.Facultades> _facultades = new List<Models.Facultades>();

            _facultades = _controller.Consulta(0).Where(x => x.fa_estado == "A").ToList();

            Facultad.DataSource = _facultades;
            Facultad.DataValueField = "fa_codigo";
            Facultad.DataTextField = "fa_descripcion";
            Facultad.DataBind();
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

        [System.Web.Services.WebMethod]
        public static string ConsultaFacultades(string parametros)
        {
            TransaccionFacultadController _controller = new TransaccionFacultadController();
            List<Models.TransaccionFacultad> _transaccionFacultades = new List<Models.TransaccionFacultad>();

            FacultadesController _controllerFacultad = new FacultadesController();
            List<Models.Facultades> _facultades = new List<Models.Facultades>();

            _transaccionFacultades = _controller.Consulta(int.Parse(parametros)).ToList();
            _facultades = _controllerFacultad.Consulta(0).Where(x => x.fa_estado == "A").ToList();

            var listaFacultades = from trnFacultad in _transaccionFacultades
                                  join facultad in _facultades on trnFacultad.tf_facultad equals facultad.fa_codigo
                                  orderby facultad.fa_codigo ascending
                                  select new { trnFacultad.tf_transaccion, trnFacultad.tf_facultad, facultad.fa_descripcion, trnFacultad.tf_estado };

            return JsonConvert.SerializeObject(listaFacultades);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarFacultad(string parametros)
        {
            TransaccionFacultadController _controller = new TransaccionFacultadController();
            Models.TransaccionFacultad parametro = new Models.TransaccionFacultad();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.tf_transaccion = int.Parse(arrayParametros[0].ToString());
            parametro.tf_facultad = Int16.Parse(arrayParametros[1].ToString());
            parametro.tf_estado = arrayParametros[2].ToString();
            parametro.tf_usuario_creacion = user_cookie.Usuario;
            parametro.tf_fecha_creacion = DateTime.Now;
            parametro.tf_usuario_actualizacion = user_cookie.Usuario;
            parametro.tf_fecha_actualizacion = DateTime.Now;

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
        public static string EliminarFacultad(string parametros)
        {
            TransaccionFacultadController _controller = new TransaccionFacultadController();
            Models.TransaccionFacultad parametro = new Models.TransaccionFacultad();

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.tf_transaccion = int.Parse(arrayParametros[0].ToString());
            parametro.tf_facultad = Int16.Parse(arrayParametros[1].ToString());
            parametro.tf_estado = arrayParametros[2].ToString();
            parametro.tf_usuario_creacion = user_cookie.Usuario;
            parametro.tf_fecha_creacion = DateTime.Now;
            parametro.tf_usuario_actualizacion = user_cookie.Usuario;
            parametro.tf_fecha_actualizacion = DateTime.Now;

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
    }
}