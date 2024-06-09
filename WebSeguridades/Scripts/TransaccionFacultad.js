async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    var strParametro = document.getElementById('Transaccion').value;

    var args = '';
    args += "'parametros':'" + strParametro + "'";
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "TransaccionFacultad.aspx/ConsultaFacultades",
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
            { field: 'tf_transaccion', headerText: 'Codigo Transaccion', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tf_facultad', headerText: 'Codigo Facultad', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'fa_descripcion', headerText: 'Facultad Asignada a Transaccion', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'tf_estado', headerText: 'Estado', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var dropdownlistbox1 = document.getElementById("Transaccion")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.tf_transaccion == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    dropdownlistbox1.disabled = "true";

    var dropdownlistbox2 = document.getElementById("Facultad")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.tf_facultad == dropdownlistbox2.options[i].value)
            dropdownlistbox2.selectedIndex = i;
    }

    dropdownlistbox2.disabled = "true";

    document.getElementById("chkEstado").checked = (args.data.tf_estado === 'A') ? true : false;
    document.getElementById('HiddenField1').value = "U";
}

function GrabarFacultad() {
    var strData;
    var strParametro;

    if (confirm("Confirma la grabación del registro de facultad?")) {
        strParametro = "";
        strParametro += document.getElementById('Transaccion').value + "|";
        strParametro += document.getElementById('Facultad').value + "|";

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
            url: "TransaccionFacultad.aspx/GrabarFacultad",
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

function EliminarFacultad() {
    var strData;
    var strParametro;

    if (confirm("Confirma la eliminación del registro de facultad?")) {
        strParametro = "";
        strParametro += document.getElementById('Transaccion').value + "|";
        strParametro += document.getElementById('Facultad').value + "|";

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
            url: "TransaccionFacultad.aspx/EliminarFacultad",
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
    var dropdownlistbox1 = document.getElementById("Transaccion");
    var dropdownlistbox2 = document.getElementById("Facultad");

    document.getElementById("chkEstado").checked = false;
    dropdownlistbox1.disabled = "false";
    dropdownlistbox2.disabled = "false";
    document.getElementById('HiddenField1').value = "I"
}