async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    var strParametro = document.getElementById('Perfil').value;

    var args = '';
    args += "'parametros':'" + strParametro + "'";
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "TransaccionPerfil.aspx/ConsultaTransacciones",
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
            { field: 'tp_perfil', headerText: 'Codigo Perfil', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tp_transaccion', headerText: 'Codigo Transaccion', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tp_grupo_opcion', headerText: 'Grupo de Opciones', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tr_descripcion', headerText: 'Transaccion', width: 200, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'go_descripcion', headerText: 'Grupo de Opciones', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tp_estado', headerText: 'Estado', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var dropdownlistbox1 = document.getElementById("Perfil")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.tp_perfil == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    dropdownlistbox1.disabled = "true";

    var dropdownlistbox2 = document.getElementById("Transaccion")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.tp_transaccion == dropdownlistbox2.options[i].value)
            dropdownlistbox2.selectedIndex = i;
    }

    dropdownlistbox2.disabled = "true";

    var dropdownlistbox3 = document.getElementById("Grupo")

    for (var i = 0; i <= dropdownlistbox3.length - 1; i++) {
        if (args.data.tp_grupo_opcion == dropdownlistbox3.options[i].value)
            dropdownlistbox3.selectedIndex = i;
    }

    document.getElementById("chkEstado").checked = (args.data.tp_estado === 'A') ? true : false;
    document.getElementById('HiddenField1').value = "U";
}

function GrabarTransaccion() {
    var strData;
    var strParametro;

    if (confirm("Confirma la grabación del registro de transaccion asociada al perfil?")) {
        strParametro = "";
        strParametro += document.getElementById('Perfil').value + "|";
        strParametro += document.getElementById('Transaccion').value + "|";
        strParametro += document.getElementById('Grupo').value + "|";

        if (document.getElementById("chkEstado").checked) {
            strParametro += "A" + "|";
        }
        else {
            strParametro += "I" + "|";
        }

        strParametro += document.getElementById('HiddenField1').value

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "TransaccionPerfil.aspx/GrabarTransaccion",
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

    if (confirm("Confirma la eliminación del registro de transaccion asociada al perfil?")) {
        strParametro = "";
        strParametro += document.getElementById('Perfil').value + "|";
        strParametro += document.getElementById('Transaccion').value + "|";
        strParametro += document.getElementById('Grupo').value + "|";
        strParametro += "X";

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "TransaccionPerfil.aspx/EliminarTransaccion",
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
    var dropdownlistbox1 = document.getElementById("Perfil");
    var dropdownlistbox2 = document.getElementById("Transaccion");

    document.getElementById('HiddenField1').value = "I"
    dropdownlistbox1.disabled = "false";
    dropdownlistbox2.disabled = "false";
    document.getElementById("chkEstado").checked = false;
}