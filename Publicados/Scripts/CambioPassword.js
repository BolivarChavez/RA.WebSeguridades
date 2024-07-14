function ValidaDatos(password1, password2) {

    if (password1 === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : La nueva contraseña no puede estar en blanco";
        $('#popupMessage').modal('show');
        return false;
    }

    if (password2 === "") {
        document.getElementById('messageContent').innerHTML = "ERROR : La confirmación de la contraseña no puede estar en blanco";
        $('#popupMessage').modal('show');
        return false;
    }

    if (password1 != password2) {
        document.getElementById('messageContent').innerHTML = "ERROR : La nueva contraseña y la confirmación no coinciden";
        $('#popupMessage').modal('show');
        return false;
    }

    return true;
}

function GrabarPassword(usuario, password1, password2) {
    var strData;
    var strParametro;

    if (!ValidaDatos(password1, password2)) {
        return strData;
    }

    if (confirm("Confirma la actualizacion de la contraseña del usuario seleccionado?")) {
        strParametro = "";
        strParametro += usuario + "|";
        strParametro += password1;

        var args = '';
        args += "'parametros':'" + strParametro + "'";
        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "CambioPassword.aspx/GrabarPassword",
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
        document.getElementById('messageContent').innerHTML = "La actualización de la contraseña del usuario ha finalizado";
        $('#popupMessage').modal('show');
    }
    else {
        document.getElementById('messageContent').innerHTML = "ERROR : " + retornoProceso[0]['mensaje'];
        $('#popupMessage').modal('show');
    }

    LlenaGrid();
    return strData;
}

function InicializaVista() {
    document.getElementById('Password1').value = "";
    document.getElementById('Password2').value = "";
}

function cierraMessagePopUp() {
    $('#popupMessage').modal('hide');
}