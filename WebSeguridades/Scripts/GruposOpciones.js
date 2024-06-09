async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "GruposOpciones.aspx/ConsultaGrupos",
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
            { field: 'go_codigo', headerText: 'Codigo', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'go_descripcion', headerText: 'Descripcion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'go_orden', headerText: 'Orden', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'go_estado', headerText: 'Estado', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.go_codigo;
    document.getElementById('Orden').value = args.data.go_orden;
    document.getElementById('Descripcion').value = args.data.go_descripcion;
    document.getElementById("chkEstado").checked = (args.data.go_estado === 'A') ? true : false;
}

function ValidaDatos() {
    var descripcion;
    var orden;

    descripcion = document.getElementById('Descripcion').value;

    if (descripcion.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado una descripción para el grupo.";
        $('#popupMessage').modal('show');
        return false;
    }

    orden = document.getElementById('Orden').value;

    if (orden.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado un orden.";
        $('#popupMessage').modal('show');
        return false;
    }

    if (!Number(orden)) {
        document.getElementById('messageContent').innerHTML = "ERROR : El orden debe ser un numero.";
        $('#popupMessage').modal('show');
        return false;
    }

    if (orden <= 0) {
        document.getElementById('messageContent').innerHTML = "ERROR : El orden debe ser un numero mayor que cero.";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarGrupo() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabación del registro de grupo de opciones?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Orden').value + "|";
        strParametro += document.getElementById('Descripcion').value + "|";

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
            url: "GruposOpciones.aspx/GrabarGrupo",
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

function EliminarGrupo() {
    var strData;
    var strParametro;

    if (confirm("Confirma la eliminación del registro de grupo de opciones?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Orden').value + "|";
        strParametro += document.getElementById('Descripcion').value + "|";
        strParametro += "X";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "GruposOpciones.aspx/GrabarGrupo",
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
    document.getElementById('Descripcion').value = "";
    document.getElementById('Orden').value = "0";
    document.getElementById("chkEstado").checked = false;
}