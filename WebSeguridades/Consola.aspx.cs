﻿using System;
using System.Web;
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

            if (user_cookie.Usuario == null || user_cookie.Usuario.Trim() == "")
            {
                Response.Redirect("ErrorAccesoOpcion.aspx", true);
            }

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        protected void Salir_ServerClick(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Cookies["userInfo"] != null)
            {
                HttpContext.Current.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseTabWindow", "CloseTabWindow()", true);
            Response.Redirect("Inicio.aspx");
        }
    }
}