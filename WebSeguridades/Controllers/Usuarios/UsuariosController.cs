﻿using System.Collections.Generic;
using WebSeguridades.Interfaces.Usuarios;
using WebSeguridades.Services.Usuarios;
using WebSeguridades.Services.Utils;

namespace WebSeguridades.Controllers.Usuarios
{
    public class UsuariosController : IUsuariosController
    {
        public string Actualizacion(Models.Usuarios usuario)
        {
            UsuariosService _usuariosService = new UsuariosService();

            return _usuariosService.Actualizacion(usuario);
        }

        public IEnumerable<Models.Usuarios> Consulta(int codigoUsuario, string nombreUsuario)
        {
            UsuariosService _usuariosService = new UsuariosService();

            return _usuariosService.Consulta(codigoUsuario, nombreUsuario);
        }

        public string Ingreso(Models.Usuarios usuario)
        {
            UsuariosService _usuariosService = new UsuariosService();
            CifradoService _cifrado = new CifradoService();

            string passwordCifrado = _cifrado.Encriptar(usuario.us_password.Trim());
            usuario.us_password = passwordCifrado;

            return _usuariosService.Ingreso(usuario);
        }
    }
}