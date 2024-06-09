using System;
using WebSeguridades.Controllers.Cookies;
using WebSeguridades.Models;

namespace WebSeguridades
{
    public partial class Consola : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
            }
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        protected void Salir_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}