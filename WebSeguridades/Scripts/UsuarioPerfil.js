async function CallServerMethod() {
    var strData;
    document.getElementById('Grid').innerHTML = "";

    var strParametro = document.getElementById('Usuario').value;

    var args = '';
    args += "'parametros':'" + strParametro + "'";
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "UsuarioPerfil.aspx/ConsultaPerfiles",
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
            { field: 'up_perfil', headerText: 'Codigo Transaccion', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'up_usuario', headerText: 'Codigo Facultad', visible: false, width: 20, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'pe_descripcion', headerText: 'Perfil', width: 250, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } },
            { field: 'up_estado', headerText: 'Estado', width: 150, textAlign: 'Left', customAttributes: { class: 'boldheadergrid' } }
        ],
        pageSettings: { pageCount: 5, pageSize: 10 },
        rowSelected: rowSelected
    });
    grid.appendTo('#Grid');
}

function rowSelected(args) {
    var dropdownlistbox1 = document.getElementById("Usuario")

    for (var i = 0; i <= dropdownlistbox1.length - 1; i++) {
        if (args.data.up_usuario == dropdownlistbox1.options[i].value)
            dropdownlistbox1.selectedIndex = i;
    }

    var dropdownlistbox2 = document.getElementById("Perfil")

    for (var i = 0; i <= dropdownlistbox2.length - 1; i++) {
        if (args.data.up_perfil == dropdownlistbox2.options[i].value)
            dropdownlistbox2.selectedIndex = i;
    }

    document.getElementById("chkEstado").checked = (args.data.up_estado === 'A') ? true : false;
    document.getElementById('HiddenField1').value = "U";
}

function GrabarPerfil() {
    var strData;
    var strParametro;

    if (confirm("Confirma la grabación del registro de perfil asociado a usuario?")) {
        strParametro = "";
        strParametro += document.getElementById('Perfil').value + "|";
        strParametro += document.getElementById('Usuario').value + "|";

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
            url: "UsuarioPerfil.aspx/GrabarPerfil",
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

function EliminarPerfil() {
    var strData;
    var strParametro;

    if (confirm("Confirma la eliminación del registro de perfil asociado a usuario?")) {
        strParametro = "";
        strParametro += document.getElementById('Perfil').value + "|";
        strParametro += document.getElementById('Usuario').value + "|";

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
            url: "UsuarioPerfil.aspx/EliminarPerfil",
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
    document.getElementById("chkEstado").checked = false;
    document.getElementById('HiddenField1').value = "I";
}