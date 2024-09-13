<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consola.aspx.cs" Inherits="WebSeguridades.Consola" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Consola de ejecución de opciones</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="anonymous" />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Styles/Custom-Opciones.css" />
    <link rel="stylesheet" href="../Styles/Custom-Menu.css" />
    <link rel="stylesheet" href="../Styles/Custom-Cards.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <script type="text/javascript">
        function noBack() { window.history.forward() }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
    </script>
</head>
<body style="margin: 0; height: 100%; overflow: hidden; background-color: #E5E8E8;">
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row header-bar-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 my-auto">
                    <img class="image-logo-empresa" src="../Images/LogoRomeroyAsociados.png" />
                </div>    
                <div class="col-sm-12 col-md-6 col-lg-6 col-xl-6 center-block text-left my-auto">
                    <p class="barra-titulo" style="color:#2C3E50;">Sistema de Control de Auditorías - Seguridades - Consola Principal</p>
                </div>  
                <div id="DivBoton" class="col-sm-12 col-md-4 col-lg-4 col-xl-4 my-auto">
                    <div class="barra-boton">
                        <div class="boton-superior">
                            <button id="Salir" name="Salir" runat="server" class="botones" style="background-color: #B7BABA;" onserverclick="Salir_ServerClick" onclick="CloseTabWindow();"><img class="imagen-boton" src="../Images/PowerButton.png" /></button>
                        </div>    
                    </div>
                </div>
            </div>
            <div class="row console-menu-height" style="background-color: #E5E8E8;">
                <div class="col-sm-12 col-md-2 col-lg-2 col-xl-2 py-4 h-100 opcion-backcolor-1" style="background-color: #B7BABA;">
                    <header class="avatar" style="background-color: #B7BABA;">
                        <asp:Label ID="labelUser" runat="server" ForeColor="#2C3E50">USUARIO</asp:Label>
                        <br />
                        <asp:Label ID="lblNombre" runat="server" ForeColor="#2C3E50">Nombre del colaborador</asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblFecha" runat="server" ForeColor="#2C3E50" Font-Size="12px">FECHA DE INGRESO</asp:Label>
                        <br />
                        <asp:Label ID="lblFechaConexion" runat="server" ForeColor="#2C3E50" Font-Size="12px">Fecha y hora del dia</asp:Label>
                    </header>
                    <br />
                    <div id="DivMenu" runat="server" class="overflow-auto">
                    </div>
                </div>
                <div class="col-sm-12 col-md-10 col-lg-10 col-xl-10 px-4 py-4 bg-white h-100 opcion-backcolor-2" style="overflow-y: scroll;">
                    <div class="container-fluid">
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/Empresa.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Mantenimiento de Empresas</h5>
                                    <p class="card-text">Configuracion de la información general de la empresa, como nombre, RUC, direccion, etc.</p>
                                    <a href="Views/Empresas.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/EmpresaOficina.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Mantenimiento de Oficinas</h5>
                                    <p class="card-text">Configuracion de las oficinas asociadas con una empresa. Se registra la información general de las oficinas, como nombre, direccion, etc.</p>
                                    <a href="Views/Oficinas.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/Facultades.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Mantenimiento de Facultades</h5>
                                    <p class="card-text">Configuracion de las facultades o acciones relacionadas a una transacción. Estas facultades se asignan a los usuarios para determinar que acciones pueden ejecutar.</p>
                                    <a href="Views/Facultades.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/GruposOpciones.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Mantenimiento de Grupos de Opciones</h5>
                                    <p class="card-text">Configuracion de grupos de opciones. Esto sirve para agrupar las opciones o transacciones asignadas a un usuario según un grupo o categoría.</p>
                                    <a href="Views/GruposOpciones.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div>  
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/UsuarioPerfiles.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Mantenimiento de Perfiles</h5>
                                    <p class="card-text">Configuracion de los perfiles de usuarios. Estos perfiles se utilizan para relacionar las transacciones autorizadas que cada uno de ellos puede ejecutar.</p>
                                    <a href="Views/Perfiles.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/Transacciones.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Mantenimiento de Transacciones</h5>
                                    <p class="card-text">Configuracion de las transacciones autorizadas. Las transacciones incluyen opciones de usuario o acciones específicas a realizarce dentro de la aplicación.</p>
                                    <a href="Views/Transacciones.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/TransaccionFacultad.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Facultades asociadas a Transacciones</h5>
                                    <p class="card-text">De las facultades que se encuentren definidas, se asocian a cada transacción aquellas facultades que dicha transacción podrá utilizar.</p>
                                    <a href="Views/TransaccionFacultad.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/TransaccionPerfil.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Transacciones asociadas a Perfiles</h5>
                                    <p class="card-text">De los perfiles que se encuentran definidos, se asocian a cada perfil aquellas transacciones que dicho perfil podrá utilizar.</p>
                                    <a href="Views/TransaccionPerfil.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div> 
                        <div class="row py-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/Usuario.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Mantenimiento de Usuarios</h5>
                                    <p class="card-text">Configuracion de la información del usuario. Incluye el nombre, el nombre de ingreso, contraseña, etc.</p>
                                    <a href="Views/Usuarios.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/Oficina.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Oficinas asociadas a Usuarios</h5>
                                    <p class="card-text">Se relacionan al usuario las oficinas a las que pueda realizar labores.</p>
                                    <a href="Views/UsuarioOficina.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/UsuarioPerfil.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Perfiles asociados a Usuarios</h5>
                                    <p class="card-text">Se relacionan al usuario los perfiles que contienen las transacciones que el usuario puede ejecutar.</p>
                                    <a href="Views/UsuarioPerfil.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/UsuarioFacultades.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Facultades asociadas a Usuarios</h5>
                                    <p class="card-text">Se relacionan al usuario las facultades que tiene autorizado ejecutar en las transacciones de sus perfiles asociados.</p>
                                    <a href="Views/UsuarioFacultad.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div> 
                        <div class="row px-2">
                            <div class="col-sm-12 col-md-3 col-lg-3 col-xl-3 d-flex align-items-stretch">
                                <div class="card">
                                  <br />
                                  <img src="Images/CambioPassword.png" class="card-img-top" alt="..." style="width:40px; height:40px; display: block; margin-left: auto; margin-right: auto;" />
                                  <div class="card-body d-flex flex-column">
                                    <h5 class="card-title">Cambio de Contraseña</h5>
                                    <p class="card-text">Permite ingresar una nueva contraseña para un usuario.</p>
                                    <a href="Views/CambioPassword.aspx" target="_blank" class="btn btn-primary mt-auto align-self-start">Acceder</a>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function CloseTabWindow() {
            for (var i = 0; i < windows.length; i++) {
                windows[i].close()
            }
        }
    </script>
</body>
</html>
