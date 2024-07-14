async function CallServerMethod() {
    var strData;

    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "Empresas.aspx/BuscarEmpresa",
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

async function BuscarEmpresa() {
    var dataGrid = await CallServerMethod();
    var empresa = JSON.parse(dataGrid);

    document.getElementById('Codigo').value = empresa[0]['em_codigo'];
    document.getElementById('Nombre').value = empresa[0]['em_nombre'];
    document.getElementById('Pais').value = empresa[0]['em_pais'];
    document.getElementById('Provincia').value = empresa[0]['em_provincia'];
    document.getElementById('Ciudad').value = empresa[0]['em_ciudad'];
    document.getElementById('Direccion').value = empresa[0]['em_direccion'];
    document.getElementById('Telefono').value = empresa[0]['em_telefono'];
    document.getElementById('Contacto').value = empresa[0]['em_contacto'];
    document.getElementById('Email').value = empresa[0]['em_email'];
}

function ValidaDatos() {
    var nombre = document.getElementById('Nombre').value;
    var pais = document.getElementById('Pais').value;
    var provincia = document.getElementById('Provincia').value;
    var ciudad = document.getElementById('Ciudad').value;
    var direccion = document.getElementById('Direccion').value;
    var telefono = document.getElementById('Telefono').value;
    var contacto = document.getElementById('Contacto').value;
    var email = document.getElementById('Email').value;

    if (nombre.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado nombre o razón social para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    if (pais.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado pais para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    if (provincia.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado provincia para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    if (ciudad.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado ciudad para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    if (direccion.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado direccion para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    if (telefono.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado telefono para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    if (contacto.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado contacto para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    if (email.trim() === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : No se ha ingresado email para la empresa";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarEmpresa() {
    var strData;
    var strParametro;

    if (!ValidaDatos()) {
        return strData;
    }

    if (confirm("Confirma la grabación del registro de empresa?")) {
        strParametro = "";
        strParametro += document.getElementById('Codigo').value + "|";
        strParametro += document.getElementById('Nombre').value + "|";
        strParametro += document.getElementById('Pais').value + "|";
        strParametro += document.getElementById('Provincia').value + "|";
        strParametro += document.getElementById('Ciudad').value + "|";
        strParametro += document.getElementById('Direccion').value + "|";
        strParametro += document.getElementById('Telefono').value + "|";
        strParametro += document.getElementById('Contacto').value + "|";
        strParametro += document.getElementById('Email').value;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "Empresas.aspx/GrabarEmpresa",
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
        BuscarEmpresa();
        document.getElementById('messageContent').innerHTML = "La grabación del registro ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    return strData;
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}
