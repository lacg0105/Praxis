$(document).ready(function () {
    var SelArea = $("#SelArea").kendoDropDownList({
        optionLabel: "Seleccione una área",
        autoBind: false,

        dataTextField: "NombreArea",
        dataValueField: "IdArea",

        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    url: "ObtenerAreas",
                    dataType: "jsonp",
                },
            },
            ServerFiltering: false,
            sort: {
                field: "NombreArea",
                dir: "asc"
            },
            schema: {
                model: {
                    fields: {
                        IdArea: { type: "number" },
                        NombreArea: { type: "string" },
                    }
                }
            },
            pageSize: 50,
        }),
    }).data("kendoDropDownList");

    $("#selRol").kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    url: "ObtenerRoles",
                    dataType: "jsonp",
                },
            },
            ServerFiltering: false,
            sort: {
                field: "Nombre",
                dir: "asc"
            },
            schema: {
                model: {
                    fields: {
                        IdRol: { type: "number" },
                        NombreRol: { type: "string" },
                    }
                }
            },
            pageSize: 50,
        }),
        dataTextField: "NombreRol",
        dataValueField: "IdRol",
        optionLabel: "Seleccione un rol",
    }).data("kendoDropDownList");

    $("#SelEstatus").kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    url: "ObtenerEstatusUsuario",
                    dataType: "jsonp",
                },
            },
            ServerFiltering: false,
            sort: {
                field: "NombreEstatus",
                dir: "asc"
            },
            schema: {
                model: {
                    fields: {
                        IdEstatusUsuario: { type: "number" },
                        NombreEstatus: { type: "string" },
                    }
                }
            },
            pageSize: 50,
        }),
        dataTextField: "NombreEstatus",
        dataValueField: "IdEstatusUsuario",
        optionLabel: "Seleccione un estatus",
    }).data("kendoDropDownList");

    $("#selInstitucion").kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    url: "ObtenerInstitucion",
                    dataType: "jsonp",
                },
            },
            ServerFiltering: false,
            sort: {
                field: "NombreInstitucion",
                dir: "asc"
            },
            schema: {
                model: {
                    fields: {
                        IdInstitucion: { type: "number" },
                        NombreInstitucion: { type: "string" },
                        IdTipoInstitucion: { type: "number" },
                        NombreTipoInstitucion: { type: "string" },
                        NombreLargo: { type: "string" },
                        Carpeta: { type: "string" }
                    }
                }
            },
            pageSize: 50,
        }),
        dataTextField: "NombreInstitucion",
        dataValueField: "IdInstitucion",
        optionLabel: "Seleccione una institución",
    }).data("kendoDropDownList");

    if ($("#hdnMov").val() == "NUEVO") {
        fnControlesEstado("INICIO");
    }
    else {
        fnControlesEstado("EDITAR");
    }

    $("#frmUser").kendoValidator({
        rules: {
            customRuleMail: function (input) {
                //only 'Tom' will be valid value for the username input
                if (input.is("[name=Correo]")) {
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
            matches: function (input) {
                var matches = input.data('matches');
                // if the `data-matches attribute was found`
                if (matches) {
                    // get the input to match
                    var match = $(matches);
                    // trim the values and check them
                    if ($.trim(input.val()) === $.trim(match.val())) {
                        // the fields match
                        return true;
                    } else {
                        // the fields don't match - validation fails
                        return false;
                    }
                }
                // don't perform any match validation on the input
                return true;
            },
        },
        messages: {
            customRuleMail: "No es una dirección de correo válida",
            matches: function (input) {
                return input.data("matchesMsg");
            },
            required: "Favor de llenar este campo.",
        }
    });

    $("#btnGuardar").click(function () {
        fnGuardar();
        return false;
    })

    $("#dpFechaNacimiento").kendoDatePicker({
        culture: "es-ES",
        weekNumber: true,
        format: "dd/MM/yyyy",
        animation: {
            close: {
                effects: "fadeOut zoom:out",
                duration: 300
            },
            open: {
                effects: "fadeIn zoom:in",
                duration: 300
            }
        }
    });

    $("#selRol").on('change', function () {
        if (this.value == 2) {
            $(".perfilUsuario").show();
        }
        else {
            $(".perfilUsuario").hide();
        }
    });

});

function fnControlesEstado(sEstado) {
    switch (sEstado) {
        case "INICIO":
            $("#txtNombre").val("");
            $("#txtUsuario").val("");
            $("#txtCorreo").val("");
            $("#txtCurp").val("");
            $("#txtPwd").val("");
            $("#txtPwdConfirm").val("");
            $("#selRol").data("kendoDropDownList").value("");
            $("#SelArea").data("kendoDropDownList").value("");
            $("#SelEstatus").data("kendoDropDownList").value("1");
            $("#txtPwd").removeAttr("required");
            $("#txtPwdConfirm").removeAttr("required");
            $("#dvPwd").hide();
            $("#dvIdStatus").hide();
            $("#PwdConfirm").hide();
            $(".perfilUsuario").hide();
            break;

        case "EDITAR":
            $("#txtPwd").removeAttr("required");
            $("#txtPwdConfirm").removeAttr("required");
            $("#txtCorreo").attr("disabled", "disabled");
            $("#selRol").data("kendoDropDownList").value($("#hdnIdRol").val());
            $("#SelArea").data("kendoDropDownList").value($("#hdnIdArea").val());
            $("#SelEstatus").data("kendoDropDownList").value($("#hdnIdStatus").val());
            $("#dvIdStatus").show();
            $("#dvPwd").hide();
            $("#PwdConfirm").hide();
            $("#selInstitucion").data("kendoDropDownList").value($("#hdnIdInstitucion").val());
            if ($("#hdnIdRol").val() == 2) {
                $(".perfilUsuario").show();
            }
            else {
                $(".perfilUsuario").hide();
            }
            break;
    }
}
//-------------------------------------------------------------------
function fnGuardar() {
    $('.ibox').children('.ibox-content').toggleClass('sk-loading');
    var validator = $("#frmUser").data("kendoValidator");
    if (validator.validate()) {

        if ($("#hdnMov").val() == "NUEVO") {
            var url = "CrearUsuario";
        }
        else {
            var url = "EditarUsuario";
        }
        //var datToSend = $("#frmUser").serialize();
        var datToSend = new FormData($('#frmUser')[0]);

        $.ajax({
            method: "POST",
            url: url,
            async: false,
            data: datToSend,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (operationResult) {
                $('.ibox').children('.ibox-content').removeClass('sk-loading');
                if (operationResult.IsSuccess == 1) {
                    //swal("Proceso completado con éxito", operationResult.Message, "success");
                    //fnControlesEstado("INICIO");
                    swal({
                        title: "Proceso completado con éxito.",
                        text: operationResult.Message,
                        type: "success",
                        showConfirmButton: true
                    },
                        function () {
                            window.location.href = $("#hdnUrlUsers").val();
                        });
                }
                else {
                    swal("Validación", operationResult.Message, "error");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.error(thrownError, 'Error en el proceso');
                $('.ibox-content').removeClass('sk-loading');
            }
        });


    }
}
//-------------------------------------------------------------------