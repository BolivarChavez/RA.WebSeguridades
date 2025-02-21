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
    public partial class CambioPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaUsuarios();
                InitializedView();
                Usuario.SelectedIndex = 0;
            }
        }

        private void InitializedView()
        {
            Password1.Value = "";
            Password2.Value = "";
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

        [System.Web.Services.WebMethod]
        public static string GrabarPassword(string parametros)
        {
            CambioPasswordController _controller = new CambioPasswordController();

            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            response = _controller.CambioPassword(int.Parse(arrayParametros[0].Trim()), arrayParametros[1].Trim());

            return response;
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            string usuario = Usuario.SelectedValue.Trim();
            string password1 = Password1.Value.Trim();
            string password2 = Password2.Value.Trim();

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarPassword('" + usuario + "','" + password1 + "','" + password2 + "');", true);
        }
    }
}