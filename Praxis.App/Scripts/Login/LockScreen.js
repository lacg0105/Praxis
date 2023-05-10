$(document).ready(function () {
    InicializarDatosUsuario(); 

    //alert($.browser.version);

    NoIntentos = 0;
    //$("#txtUsuario").change(function () {
    //    NoIntentos = 0;
    //});
    $("#btnIngresar").click(function (event) {
        event.preventDefault();
        fnIngresar();
        return false;
    });
    $("#lnkOlvidoContra").click(function () {
        fnRecuperarContraseña();
        return true;
    });
    var container = $("#frmLogin2Form");
    kendo.init(container);
    container.kendoValidator({
        rules: {
            customRuleMail: function (input) {
                //only 'Tom' will be valid value for the username input
                if (input.is("[name=Mail]")) {
                    var str = input.val();
                    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
                    var res = pattern.test(str);

                    if (res == true) {
                        return true;
                    } else {
                        return false;
                    }
                }
                return true;
            },
            greaterdate: function (input) {
                if (input.is("[data-greaterdate-msg]") && input.val() != "") {
                    var date = kendo.parseDate(input.val()),
                        otherDate = kendo.parseDate($("[name='" + input.data("greaterdateField") + "']").val());
                    return otherDate == null || otherDate.getTime() < date.getTime();
                }

                return true;
            }
        }
        ,
        messages: {
            customRuleMail: "No es una dirección de correo válida",
            matches: function (input) {
                return input.data("matchesMsg");
            },
            required: "Favor de llenar este campo.",
        }
    });
    $("#btnRegresar").click(function (event) {
        window.location.href = $("#urlRedirectLogin").val();
    });
});
//-----------------------------------------------------
function fnRecuperarContraseña() {
    swal({
        title: "¿Desea continuar?",
        text: "Se iniciara un proceso para la recuperación de su contraseña",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#AC1E8F",
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                $('.FormsLogin').toggleClass('sk-loading');
                var datToSend = new FormData($('#frmLogin2Form')[0]);
                datToSend.append("Correo", localStorage.Correo);
                $.ajax({
                    type: "POST",
                    url: "RecuperarCuenta",
                    contentType: false,
                    processData: false,
                    data: datToSend,
                    success: function (operationResult) {
                        $('.FormsLogin').removeClass('sk-loading');
                        if (operationResult.IsSuccess == 1) {
                            $("#txtUsuario").val("");
                            $("#txtPassword").val("");
                            swal("Recuperación en proceso", operationResult.Message, "success");
                        }
                        else {
                            // localStorage.clear();
                            swal("Validación", operationResult.Message, "error");
                            $('.FormsLogin').removeClass('sk-loading');
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        //localStorage.clear();
                        toastr.error(thrownError, 'Error en el proceso');
                        $('.FormsLogin').removeClass('sk-loading');
                    }
                });
            }
        });

}
//-----------------------------------------------------
function InicializarDatosUsuario() {
    localStorage.UsuarioBloqueadoLockScreen = 0;
    document.getElementById("Nombre").innerHTML = localStorage.Nombre;
    document.getElementById("Curp").innerHTML = localStorage.Curp;
    document.getElementById("NombreRol").innerHTML = localStorage.NombreRol;
    if (localStorage.NombreRol === "Medico") {
        $("#CedulaProfesional").css("display", "block");
        document.getElementById("Cedula").innerHTML = localStorage.Cedula;
    }
    $("#imagenUsuario").attr("src", "/Content/Img/Usuarios/" + localStorage.NombreImagen);
}
//-----------------------------------------------------
function fnIngresar() {
    var validator = $("#frmLogin2Form").data("kendoValidator");
    if (validator.validate()) {
        $('.FormsLogin').toggleClass('sk-loading');
        var datToSend = new FormData($('#frmLogin2Form')[0]);
        datToSend.append("Correo", localStorage.Correo);
        datToSend.append("IdEstatusUsuario", localStorage.IdEstatusUsuario);
        $.ajax({
            type: "POST",
            url: "IngresaSistema",
            contentType: false,
            processData: false,
            data: datToSend,
            success: function (operationResult) {
                $('.FormsLogin').removeClass('sk-loading');
                if (operationResult.IsSuccess == 1) {
                    window.location.href = $("#urlRedirect").val();
                }
                else {
                    if (operationResult.Message == "La contraseña es incorrecta.") {
                        NoIntentos = NoIntentos + 1;
                        if (NoIntentos > 3) {
                            fnBloquearUsuario();
                        }
                        else {
                            swal("Validación", "La contraseña es incorrecta, recuerde que el máximo número de intentos son 3 antes de que su usuario sea bloqueado. \n Este es su intento No." + NoIntentos + "", "error");
                        }

                    }
                    else {
                        localStorage.UsuarioBloqueadoLockScreen = 3;
                        NoIntentos = 0;
                        $("#txtPassword").val("");
                        window.location.href = $("#urlRedirectLogin").val();
                    }
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                // localStorage.clear();
                toastr.error(thrownError, 'Error en el proceso');
                $('.FormsLogin').removeClass('sk-loading');
            }
        });

    }
}
//-----------------------------------------------------
function fnBloquearUsuario() {
    var datToSend = new FormData($('#frmLogin2Form')[0]);
    datToSend.append("Correo", localStorage.Correo);
    $.ajax({
        type: "POST",
        url: "BloquearUsuario",
        contentType: false,
        processData: false,
        data: datToSend,
        success: function (operationResult) {
            $('#ibox2').children('.ibox-content').removeClass('sk-loading');
            if (operationResult.IsSuccess == 1) {
                NoIntentos = 0;
                $("#txtPassword").val("");
                localStorage.UsuarioBloqueadoLockScreen = 1;
                window.location.href = $("#urlRedirectLogin").val();
                return false;
            }
            else {

                swal("Validación", operationResult.Message, "error");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //localStorage.clear();
            toastr.error(thrownError, 'Error en el proceso');
            $('#ibox2').children('.ibox-content').removeClass('sk-loading');
        }
    });
}
//-----------------------------------------------------