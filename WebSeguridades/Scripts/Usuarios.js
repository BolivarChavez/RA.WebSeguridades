async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Usuarios.aspx/ConsultaUsuarios",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d != '') {
                strData = response.d;
            }
        },
        fail: function (response) {
            debugger;
            alert(response.d);
        }
    });

    return strData;
}

async function LlenaGrid() {
    var dataGrid = await CallServerMethod();

    var contenedorHeight = document.getElementById('Contenedor').clientHeight;
    var formHeight = document.getElementById('Formulario').clientHeight;
    var divGrid = document.getElementById("GridConsulta");
    divGrid.style.height = contenedorHeight - formHeight - 10 + "px";

    ej.base.registerLicense('ORg4AjUWIQA/Gnt2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn9TdkNiX3xZc31TRWZb');
    ej.base.enableRipple(true);

    var grid = new ej.grids.Grid({
        dataSource: JSON.parse(dataGrid),
        toolbar: ['Search'],
        allowPaging: false,
        allowScrolling: true,
        height: '100%',
        columns: [
            { field: 'us_codigo', headerText: 'Codigo', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'us_nombre', headerText: 'Nombre Completo', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'us_login', headerText: 'Nombre de Usuario', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'us_password', headerText: 'Contraseña', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'us_email', headerText: 'Correo Electronico', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'us_estado', headerText: 'Estado', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.us_codigo;
    document.getElementById('Nombre').value = args.data.us_nombre;
    document.getElementById('Login').value = args.data.us_login;
    document.getElementById('Password').value = args.data.us_password;
    document.getElementById('Email').value = args.data.us_email;
    document.getElementById("chkEstado").checked = (args.data.us_estado === 'A') ? true : false;

    document.getElementById('HiddenField1').value = args.data.us_password;
    document.getElementById('Password').style.display = 'none';
    document.getElementById("lblPassword").style.display = 'none';
}

function ValidaDatos() {
    var descripcion;
    var login;
    var password;

    descripcion = document.getElementById('Nombre').value;

    if (descripcion.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado un nombre completo para el usuario";
        $('#popupMessage').modal('show');
        return false;
    }

    login = document.getElementById('Login').value;

    if (login.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado un nombre de usuario";
        $('#popupMessage').modal('show');
        return false;
    }

    password = document.getElementById('Password').value;

    if (password.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado una contraseña para el usuario";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarUsuario() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabación del registro de usuario?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Nombre').value + "|";
        strParametro += document.getElementById('Login').value + "|";
        strParametro += document.getElementById('Password').value + "|";
        strParametro += document.getElementById('Email').value + "|";

        if (document.getElementById("chkEstado").checked) {
            strParametro += "A";
        }
        else {
            strParametro += "I";
        }

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Usuarios.aspx/GrabarUsuario",
            data: "{" + args + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d != '') {
                    strData = response.d;
                }
            },
            fail: function (response) {
                debugger;
                alert(response.d);
            }
        });
    }
    else {
        strData = "";
    }

    var retornoProceso = JSON.parse(strData)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();
        document.getElementById('messageContent').innerHTML = "La grabación del registro ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    LlenaGrid();
    return strData;
}

function EliminarUsuario() {
    var strData;
    var strParametro;

    if (confirm("Confirma la eliminación del registro de usuario?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Nombre').value + "|";
        strParametro += document.getElementById('Login').value + "|";
        strParametro += document.getElementById('Password').value + "|";
        strParametro += document.getElementById('Email').value + "|";
        strParametro += "X";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Usuarios.aspx/GrabarUsuario",
            data: "{" + args + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d != '') {
                    strData = response.d;
                }
            },
            fail: function (response) {
                debugger;
                alert(response.d);
            }
        });
    }
    else {
        strData = "";
    }

    var retornoProceso = JSON.parse(strData)

    if (retornoProceso[0]['retorno'] === 0) {
        InicializaVista();
        document.getElementById('messageContent').innerHTML = "La eliminación del registro ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    LlenaGrid();
    return strData;
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}

function InicializaVista() {
    document.getElementById('Codigo').value = "0";
    document.getElementById('Nombre').value = "";
    document.getElementById('Login').value = "";
    document.getElementById('Password').value = "";
    document.getElementById('Email').value = "";
    document.getElementById("chkEstado").checked = false;
    document.getElementById('HiddenField1').value = "";
    document.getElementById('Password').style.display = '';
    document.getElementById("lblPassword").style.display = '';
}
