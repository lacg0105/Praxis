$(document).ready(function () {

    CargaWizard();   

    //$(".select2_").select2();

    fnObtenerSexo();
    fnObtenerReligion();
    fnObtenerTipoSangre();

    //$('#data_1 .input-group.date').datepicker({
    //    todayBtn: "linked",
    //    keyboardNavigation: false,
    //    forceParse: false,
    //    calendarWeeks: true,
    //    autoclose: true
    //});

    $(".fechas").kendoDatePicker({
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

    $(".numero").kendoNumericTextBox();
});
//-------------------------------------------------------------------
function CargaWizard() {
    $("#wizard").steps();
    $("#form").steps({
        bodyTag: "fieldset",
        onStepChanging: function (event, currentIndex, newIndex) {
            // Always allow going backward even if the current step contains invalid fields!
            if (currentIndex > newIndex) {
                return true;
            }

            // Forbid suppressing "Warning" step if the user is to young
            if (newIndex === 3 && Number($("#age").val()) < 18) {
                return false;
            }

            var form = $(this);

            // Clean up if user went backward before
            if (currentIndex < newIndex) {
                // To remove error styles
                $(".body:eq(" + newIndex + ") label.error", form).remove();
                $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
            }

            // Disable validation on fields that are disabled or hidden.
            form.validate().settings.ignore = ":disabled,:hidden";

            // Start validation; Prevent going forward if false
            return form.valid();
        },
        onStepChanged: function (event, currentIndex, priorIndex) {
            // Suppress (skip) "Warning" step if the user is old enough.
            if (currentIndex === 2 && Number($("#age").val()) >= 18) {
                $(this).steps("next");
            }

            // Suppress (skip) "Warning" step if the user is old enough and wants to the previous step.
            if (currentIndex === 2 && priorIndex === 3) {
                $(this).steps("previous");
            }
        },
        onFinishing: function (event, currentIndex) {
            var form = $(this);

            // Disable validation on fields that are disabled.
            // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
            form.validate().settings.ignore = ":disabled";

            // Start validation; Prevent form submission if false
            return form.valid();
        },
        onFinished: function (event, currentIndex) {
            var form = $(this);

            // Submit form input
            form.submit();
        }
    }).validate({
        messages: {
            Nombre: "Ingrese su nombre.",
            FechaNacimiento: "Seleccione una fecha.",
        },
        //errorPlacement: function (error, element) {
        //    element.before(error);
        //},
        rules: {
            Nombre: "required",
            FechaNacimiento: "required",
            confirm: {
                equalTo: "#password"
            }
        },
        errorPlacement: function (error, element) {
            // Add the `help-block` class to the error element
            error.addClass("help-block");

            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-12").addClass("has-error").removeClass("has-success");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-12").addClass("has-success").removeClass("has-error");
        }
    });
}
//-------------------------------------------------------------------
function fnObtenerSexo() {

    var urlSexo = $("#hdnUrlSexo").val();
    var SelSexo = $("#selSexo").kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    url: urlSexo,
                    dataType: "jsonp",
                },
            },
            ServerFiltering: false,
            sort: {
                field: "NombreSexo",
                dir: "asc"
            },
            schema: {
                model: {
                    fields: {
                        IdSexo: { type: "number" },
                        NombreSexo: { type: "string" },
                    }
                }
            },
            //group: { field: "TipoInstitucionFinanciera" },
            pageSize: 100,
        }),
        //template: '<span class="k-state-default"><strong>#: data.Nombre #</strong> (#: data.TipoInstitucionFinanciera #)</span>',
        dataTextField: "NombreSexo",
        dataValueField: "IdSexo",
        optionLabel: "Seleccione el sexo",
        //select: onSelectInstituciones,
    }).data("kendoDropDownList");
}
//-------------------------------------------------------------------
function fnObtenerReligion() {

    var urlReligion = $("#hdnUrlReligion").val();
    var SelReligion = $("#selReligion").kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    url: urlReligion,
                    dataType: "jsonp",
                },
            },
            ServerFiltering: false,
            sort: {
                field: "NombreReligion",
                dir: "asc"
            },
            schema: {
                model: {
                    fields: {
                        IdReligion: { type: "number" },
                        NombreReligion: { type: "string" },
                    }
                }
            },
            //group: { field: "TipoInstitucionFinanciera" },
            pageSize: 100,
        }),
        //template: '<span class="k-state-default"><strong>#: data.Nombre #</strong> (#: data.TipoInstitucionFinanciera #)</span>',
        dataTextField: "NombreReligion",
        dataValueField: "IdReligion",
        optionLabel: "Seleccione una religión",
        //select: onSelectInstituciones,
    }).data("kendoDropDownList");
}
//-----------------------------------------------------
function fnObtenerTipoSangre() {

    var urlTipoSangre = $("#hdnUrlTipoSangre").val();
    var SelTipoSangre = $("#selTipoSangre").kendoDropDownList({
        dataSource: new kendo.data.DataSource({
            transport: {
                read: {
                    url: urlTipoSangre,
                    dataType: "jsonp",
                },
            },
            ServerFiltering: false,
            sort: {
                field: "NombreTipoSangre",
                dir: "asc"
            },
            schema: {
                model: {
                    fields: {
                        IdTipoSangre: { type: "number" },
                        NombreTipoSangre: { type: "string" },
                    }
                }
            },
            //group: { field: "TipoInstitucionFinanciera" },
            pageSize: 100,
        }),
        //template: '<span class="k-state-default"><strong>#: data.Nombre #</strong> (#: data.TipoInstitucionFinanciera #)</span>',
        dataTextField: "NombreTipoSangre",
        dataValueField: "IdTipoSangre",
        optionLabel: "Seleccione el tipo de sangre",
        //select: onSelectInstituciones,
    }).data("kendoDropDownList");
}
//-----------------------------------------------------