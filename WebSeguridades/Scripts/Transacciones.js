async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Transacciones.aspx/ConsultaTransacciones",
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
            { field: 'tr_codigo', headerText: 'Codigo', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tr_descripcion', headerText: 'Descripcion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tr_descripcion_larga', headerText: 'Descripcion Larga', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tr_tipo', headerText: 'Tipo', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tr_programa', headerText: 'Tipo', visible: false, width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tr_estado', headerText: 'Estado', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    document.getElementById('Codigo').value = args.data.tr_codigo;
    document.getElementById('Descripcion').value = args.data.tr_descripcion;
    document.getElementById('DescripcionLarga').value = args.data.tr_descripcion_larga;
    document.getElementById('Programa').value = args.data.tr_programa;

    var dropdownlistbox = document.getElementById("Tipo")

    for (var i = 0; i <= dropdownlistbox.length - 1; i++) {
        if (args.data.tr_tipo == dropdownlistbox.options[i].value)
            dropdownlistbox.selectedIndex = i;
    }

    document.getElementById("chkEstado").checked = (args.data.tr_estado === 'A') ? true : false;
}

function ValidaDatos() {
    var descripcion;
    var descripcionLarga;

    descripcion = document.getElementById('Descripcion').value;

    if (descripcion.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado una descripción para la transaccion";
        $('#popupMessage').modal('show');
        return false;
    }

    descripcionLarga = document.getElementById('DescripcionLarga').value;

    if (descripcionLarga.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado una descripción larga para la transaccion";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarTransaccion() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabación del registro de la transaccion?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Descripcion').value + "|";
        strParametro += document.getElementById('DescripcionLarga').value + "|";
        strParametro += document.getElementById('Tipo').value + "|";
        strParametro += document.getElementById('Programa').value + "|";

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
            url: "Transacciones.aspx/GrabarTransaccion",
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

function EliminarTransaccion() {
    var strData;
    var strParametro;

    if (confirm("Confirma la eliminación del registro de la transaccion?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Descripcion').value + "|";
        strParametro += document.getElementById('DescripcionLarga').value + "|";
        strParametro += document.getElementById('Tipo').value + "|";
        strParametro += document.getElementById('Programa').value + "|";
        strParametro += "X";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Transacciones.aspx/GrabarTransaccion",
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
    document.getElementById('DescripcionLarga').value = "";
    document.getElementById('Programa').value = "";
    document.getElementById("chkEstado").checked = false;
}