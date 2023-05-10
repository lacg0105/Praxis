var NoIntentos = 0;
//-----------------------------------------------------
$(document).ready(function () {
    //alert($.browser.version);
    if (localStorage.UsuarioBloqueadoLockScreen == 1) {
        swal("Validación", "Ha superado el número de intentos, su cuenta ha sido bloqueada por seguridad. Por favor contacte al administrador del sistema.", "error");
    } else if (localStorage.UsuarioBloqueadoLockScreen == 3) {
        swal("Validación", "El usuario no se encuentra activo, por favor contacte al administrador del sistema.", "error");
    } else if (localStorage.IdTemp == 1) {
        swal("Validación", "Se reestableció la contraseña exitosamente.", "success");
    }
    localStorage.clear();
    

    $("#btnIngresar").click(function () {
        fnIngresar();
        return false;
    });
    $("#lnkOlvidoContra").click(function () {
        fnRecuperarContraseña();
        return true;
    });
    var container = $("#frmLoginForm");
    kendo.init(container);
    container.kendoValidator({
        rules: {
            customRuleInputs: function (input) {
                //only 'Tom' will be valid value for the username input
                if (input.is("[name=Correo]")) {
                    if ($.trim(input.val()) !== "") {
                        return true;
                    } else {
                        if ($.trim($("#txtCurp").val()) !== "") {
                            return true;
                        }
                    }
                }

                if (input.is("[name=Curp]")) {
                    if ($.trim(input.val()) !== "") {
                        return true;
                    } else {
                        if ($.trim($("#txtUsuario").val()) !== "") {
                            return true;
                        }
                    }
                }
                return false;
            },
            customRuleMail: function (input) {
                if (input.is("[name=Correo]") && $.trim($("#txtCurp").val()) === "") {
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
            customRuleInputs: "Favor de ingresar su correo o curp",
            matches: function (input) {
                return input.data("matchesMsg");
            },
            //required: "Favor de llenar este campo.",
        }
    });
});
//-----------------------------------------------------
function fnIngresar() {
    var validator = $("#frmLoginForm").data("kendoValidator");
    if (validator.validate()) {
        $('.FormsLogin').toggleClass('sk-loading');
            var datToSend = $("#frmLoginForm").serialize();
            $.ajax({
                method: "POST",
                url: "ValidarUsuario",
                async: false,
                data: datToSend,
                success: function (operationResult) {
                    $('.FormsLogin').removeClass('sk-loading');
                    if (operationResult.IsSuccess == 1) {
                        localStorage.Correo = operationResult.Data.Correo;
                        localStorage.NombreImagen = operationResult.Data.NombreImagen;
                        localStorage.Nombre = operationResult.Data.Nombre;
                        localStorage.Curp = operationResult.Data.Curp;
                        localStorage.NombreRol = operationResult.Data.NombreRol;
                        localStorage.IdEstatusUsuario = operationResult.Data.IdEstatusUsuario;
                        localStorage.Cedula = operationResult.Data.Cedula;
                        window.location.href = $("#urlRedirect").val();
                    }
                    else {

                        localStorage.clear();
                        swal("Error", operationResult.Message, "error");
                        $('.FormsLogin').removeClass('sk-loading');
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
