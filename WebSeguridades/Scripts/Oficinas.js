async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Oficinas.aspx/ConsultaOficinas",
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
            { field: 'of_codigo', headerText: 'Codigo', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_nombre', headerText: 'Descripcion', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_pais', headerText: 'Descripcion', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_provincia', headerText: 'Descripcion', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_ciudad', headerText: 'Descripcion', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_direccion', headerText: 'Descripcion', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_telefono', headerText: 'Descripcion', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_contacto', headerText: 'Descripcion', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_email', headerText: 'Descripcion', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'of_estado', headerText: 'Estado', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.of_codigo;
    document.getElementById('Nombre').value = args.data.of_nombre;
    document.getElementById('Pais').value = args.data.of_pais;
    document.getElementById('Provincia').value = args.data.of_provincia;
    document.getElementById('Ciudad').value = args.data.of_ciudad;
    document.getElementById('Direccion').value = args.data.of_direccion;
    document.getElementById('Telefono').value = args.data.of_telefono;
    document.getElementById('Contacto').value = args.data.of_contacto;
    document.getElementById('Email').value = args.data.of_email;
    document.getElementById("chkEstado").checked = (args.data.of_estado === 'A') ? true : false;
}

function ValidaDatos() {
    var nombre;
    var pais;
    var provincia;
    var ciudad;
    var direccion;
    var telefono;
    var contacto;
    var email;

    nombre = document.getElementById('Nombre').value;

    if (nombre.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado un nombre para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    pais = document.getElementById('Pais').value;

    if (pais.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado el país para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    provincia = document.getElementById('Provincia').value;

    if (provincia.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado la provincia para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    ciudad = document.getElementById('Ciudad').value;

    if (ciudad.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado la ciudad para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    direccion = document.getElementById('Direccion').value;

    if (direccion.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado la direccion para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    telefono = document.getElementById('Telefono').value;

    if (telefono.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado el telefono para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    contacto = document.getElementById('Contacto').value;

    if (contacto.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado el contacto para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    email = document.getElementById('Email').value;

    if (email.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado el correo electrónico para la oficina";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarOficina() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabación del registro de oficina?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Nombre').value + "|";
        strParametro += document.getElementById('Pais').value + "|";
        strParametro += document.getElementById('Provincia').value + "|";
        strParametro += document.getElementById('Ciudad').value + "|";
        strParametro += document.getElementById('Direccion').value + "|";
        strParametro += document.getElementById('Telefono').value + "|";
        strParametro += document.getElementById('Contacto').value + "|";
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
            url: "Oficinas.aspx/GrabarOficina",
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

function EliminarOficina() {
    var strData;
    var strParametro;

    if (confirm("Confirma la eliminación del registro de oficina?")) {
        strParametro = "";
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Nombre').value + "|";
        strParametro += document.getElementById('Pais').value + "|";
        strParametro += document.getElementById('Provincia').value + "|";
        strParametro += document.getElementById('Ciudad').value + "|";
        strParametro += document.getElementById('Direccion').value + "|";
        strParametro += document.getElementById('Telefono').value + "|";
        strParametro += document.getElementById('Contacto').value + "|";
        strParametro += document.getElementById('Email').value + "|";
        strParametro += "X";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Oficinas.aspx/GrabarOficina",
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
    document.getElementById('Pais').value = "";
    document.getElementById('Provincia').value = "";
    document.getElementById('Ciudad').value = "";
    document.getElementById('Direccion').value = "";
    document.getElementById('Telefono').value = "";
    document.getElementById('Contacto').value = "";
    document.getElementById('Email').value = "";
    document.getElementById("chkEstado").checked = false;
}