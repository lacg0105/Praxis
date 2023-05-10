$(document).ready(function () {
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");
    //$("#wizard").kendoWizard({
    //    pager: true,
    //    done: function (e) {
    //        e.originalEvent.preventDefault();
    //        kendo.alert("Thank you for registering! Registration details will be sent to your email.");
    //    },
    //    steps: [
    //        {
    //            title: "Identificación",
    //buttons: ["Siguiente"],
    //content: '<strong>FICHA TÉCNICA</strong>',
    //form: {
    //    orientation: "horizontal",
    //    formData: {
    //        Username: "johny",
    //        Email: "john.doe@email.com",
    //        Password: "pass123",
    //        Birth: new Date()
    //    },
    //    items: [
    //        { field: "Username", label: "Username:", validation: { required: true } },
    //        { field: "Email", label: "Email:", validation: { required: true, email: true } },
    //        { field: "Password", label: "Password:", validation: { required: true }, hint: "Hint: enter alphanumeric characters only.", attributes: { type: "password" } },
    //        { field: "Birth", label: { text: "Date of Birth:", optional: true } }
    //    ]

    //}
    //},
    //{
    //    title: "Personal Details",
    //    buttons: ["previous", "next"],
    //    form: {
    //        orientation: "vertical",
    //        items: [
    //            { field: "FullName", label: "Full Name:", validation: { required: true } },
    //            {
    //                field: "Country", label: "Country:", validation: { required: true }, editor: "AutoComplete",
    //                editorOptions: {
    //                    dataSource: countries,
    //                    filter: "startswith",
    //                    placeholder: "Select country..."
    //                }
    //            },
    //            {
    //                field: "Gender",
    //                label: "Gender:",
    //                validation: { required: true },
    //                editor: "RadioGroup",
    //                editorOptions: {
    //                    items: ["Female", "Male", "Other"],
    //                    layout: "horizontal",
    //                    labelPosition: "before"
    //                }
    //            },
    //            { field: "About", label: { text: "About:", optional: true } }
    //        ]
    //    }
    //},
    //{
    //    title: "Payment Details",
    //    buttons: ["previous", "done"],
    //    form: {
    //        orientation: "vertical",
    //        items: [{
    //            type: "group",
    //            label: "Payment Details:",
    //            layout: "grid",
    //            grid: { cols: 3, gutter: 10 },
    //            items: [
    //                {
    //                    field: "PaymentType",
    //                    label: "Payment Type:",
    //                    validation: {
    //                        required: true,
    //                        payment: function (input) {
    //                            if (input.is("[name='PaymentType']") && input.attr("required")) {
    //                                input.attr("data-payment-msg", "Payment Type is required");

    //                                return input.parents('[data-role="form"]').find("[type=radio][name=" + input.attr("name") + "]").is(":checked");
    //                            }

    //                            return true;
    //                        }
    //                    },
    //                    editor: paymentEditor,
    //                    colSpan: 3
    //                },
    //                {
    //                    field: "CardNumber",
    //                    label: "Card Number:",
    //                    attributes: { "data-validmaskNumber-Msg": "Card number is incomplete" },
    //                    validation: {
    //                        required: true,
    //                        validmaskNumber: function (input) {
    //                            if (input.is("[name='CardNumber']") && input.val() != "") {
    //                                var maskedtextbox = input.data("kendoMaskedTextBox");
    //                                return maskedtextbox.value().indexOf(maskedtextbox.options.promptChar) === -1;
    //                            }

    //                            return true;
    //                        }
    //                    },
    //                    editor: "MaskedTextBox",
    //                    editorOptions: { mask: "0000-0000-0000-0000" },
    //                    colSpan: 2
    //                },
    //                {
    //                    field: "CSVNumber",
    //                    label: "CSV Number:",
    //                    attributes: { "data-validmaskCSV-Msg": "CSV code is incomplete" },
    //                    validation: {
    //                        required: true,
    //                        validmaskCSV: function (input) {
    //                            if (input.is("[name='CSVNumber']") && input.val() != "") {
    //                                var maskedtextbox = input.data("kendoMaskedTextBox");
    //                                return maskedtextbox.value().indexOf(maskedtextbox.options.promptChar) === -1;
    //                            }

    //                            return true;
    //                        }
    //                    },
    //                    editor: "MaskedTextBox",
    //                    editorOptions: { mask: "000" },
    //                    colSpan: 1,
    //                    hint: "The last 3 digids on the back"
    //                },
    //                {
    //                    field: "ExpirationDate",
    //                    label: "Expiration Date:",
    //                    validation: {
    //                        required: true,
    //                    },
    //                    editor: "DateInput",
    //                    editorOptions: {
    //                        format: "MM/yyyy"
    //                    },
    //                    colSpan: 3
    //                },
    //                {
    //                    field: "CardHolderName",
    //                    label: "Card Holder Name:",
    //                    validation: {
    //                        required: true
    //                    },
    //                    colSpan: 3
    //                },
    //            ]
    //        }]
    //    }
    //}
    //    ]
    //});  

    //$("#wizard").kendoForm({
    //    validatable: { validationSummary: true },
    //    orientation: "vertical",
    //    formData: {
    //        TextBox: "John Doe",
    //        NumericTextBox: 2,
    //        MaskedTextBox: 21313,
    //        DatePicker: new Date(),
    //        DateTimePicker: new Date(),
    //        Switch: true,
    //        DropDownList: 1
    //    },
    //    items: [
    //        {
    //            field: "TextBox",
    //            label: "TextBox",
    //            validation: { required: true }
    //        },
    //        {
    //            field: "NumericTextBox",
    //            editor: "NumericTextBox",
    //            label: "NumericTextBox",
    //            validation: { required: true }
    //        },
    //        {
    //            field: "MaskedTextBox",
    //            editor: "MaskedTextBox",
    //            label: "MaskedTextBox",
    //            validation: { required: true }
    //        },
    //        {
    //            field: "DatePicker",
    //            editor: "DatePicker",
    //            label: "Date Picker:",
    //            validation: { required: true }
    //        },
    //        {
    //            field: "DateTimePicker",
    //            editor: "DateTimePicker",
    //            label: "Date Time Picker:",
    //            validation: { required: true }
    //        },
    //        {
    //            field: "Switch",
    //            editor: "Switch",
    //            label: "Switch",
    //            validation: { required: true }
    //        },
    //        {
    //            field: "DropDownList", editor: "DropDownList", label: "DropDownList", validation: { required: true }, editorOptions: {
    //                optionLabel: "Select item...",
    //                dataSource: [
    //                    { Name: "Item1", Id: 1 },
    //                    { Name: "Item2", Id: 2 }
    //                ],
    //                dataTextField: "Name",
    //                dataValueField: "Id"
    //            }
    //        }
    //    ]
    //});

    $("#form").steps({
        bodyTag: "fieldset",
        labels: {
            //cancel: "Cancel",
            //current: "current step:",
            //pagination: "Pagination",
            finish: "Guardar",
            next: "Siguiente",
            previous: "Previo",
            //loading: "Loading ..."
        },
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
            $('.ibox-content').toggleClass('sk-loading');
            var datToSend = new FormData($('#form')[0]);
            if ($("#hdnMov").val() == "Crear") {
                var url = "CrearHistoriaClinica";
            }
            else {
                var url = "EditarHistoriaClinica";
            }

            $.ajax({
                method: "POST",
                url: url,
                async: false,
                data: datToSend,
                type: 'POST',
                contentType: false,
                processData: false,
                success: function (operationResult) {
                    $('.ibox-content').removeClass('sk-loading');
                    if (operationResult.IsSuccess == 1) {
                        //swal("Proceso completado con éxito", operationResult.Message, "success");
                        //fnControlesEstado("INICIO");
                        swal({
                            title: "Historia Clínica Editada.",
                            text: operationResult.Message,
                            type: "success",
                            showConfirmButton: true
                        },
                            function () {
                                window.location.href = $("#hdnUrlAdministracion").val();
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

            // Submit form input
            //form.submit();
        }
    }).validate({
        errorPlacement: function (error, element) {
            element.before(error);
        },
        rules: {
            confirm: {
                equalTo: "#password"
            },
            IdSexo: { valueNotEquals: "0" },
        },
        messages: {
            IdSexo: { valueNotEquals: "Please select an item!" }
        }
    });
    jQuery.extend(jQuery.validator.messages, {
        required: "Este campo es requerido.",
        //remote: "Please fix this field.",
        //email: "Please enter a valid email address.",
        //url: "Please enter a valid URL.",
        //date: "Please enter a valid date.",
        //dateISO: "Please enter a valid date (ISO).",
        //number: "Please enter a valid number.",
        //digits: "Please enter only digits.",
        //creditcard: "Please enter a valid credit card number.",
        //equalTo: "Please enter the same value again.",
        //accept: "Please enter a value with a valid extension.",
        //maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
        //minlength: jQuery.validator.format("Please enter at least {0} characters."),
        //rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
        //range: jQuery.validator.format("Please enter a value between {0} and {1}."),
        //max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
        //min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
    });
    $("#dpFechaNacimiento").kendoDatePicker({
        culture: "es-ES",
        //value: new Date(),
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
    $("#dpFechaNacimiento").attr("readonly", true);
    $('[data-toggle="tooltip"]').tooltip();
    //$(".select2_demo_1").select2();
    fnObtenerSexo();
    fnObtenerEscolaridad();
    fnObtenerReligion();
    fnObtenerEstadoCivil();
    fnObtenerTipoSangre();
    //fnControlesEstado("Inicio");
    fnDefinirGrid();

    $("#selSexo").change(function () {
        switch ($("#selSexo").val()) {
            case "1":
                fnControlesEstado("Hombre");
                break;
            case "2":
                fnControlesEstado("Mujer");
                break;
            default:
                break;
        }
    });

    if ($("#hdnMov").val() == "Crear") {
        fnControlesEstado("Inicio");
    }
    else {
        fnControlesEstado("Editar");
    }

});
//-------------------------------------------------------------------
//function fnObtenerSexo() {
//    var urlSexo = $("#hdnUrlSexo").val();
//    $("#selSexo").kendoDropDownList({
//        dataSource: new kendo.data.DataSource({
//            transport: {
//                read: {
//                    url: urlSexo,
//                    dataType: "jsonp",
//                },
//            },
//            ServerFiltering: false,
//            sort: {
//                field: "NombreSexo",
//                dir: "asc"
//            },
//            schema: {
//                model: {
//                    fields: {
//                        IdSexo: { type: "number" },
//                        NombreSexo: { type: "string" },
//                    }
//                }
//            },
//            pageSize: 50,
//        }),
//        dataTextField: "NombreSexo",
//        dataValueField: "IdSexo",
//        optionLabel: {
//            NombreSexo: "Select a product...",
//            IdSexo: "0"
//        }
//    }).data("kendoDropDownList");
//}
//-------------------------------------------------------------------
function fnObtenerSexo() {
    var urlSexo = $("#hdnUrlSexo").val();
    $("#selSexo").select2({
        minimumResultsForSearch: -1,
        placeholder: 'Selecciona el sexo',
        ajax: {
            url: urlSexo,
            dataType: 'json',
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
        }
    });
}
//-------------------------------------------------------------------
function fnObtenerEscolaridad() {
    var urlEscolaridad = $("#hdnUrlEscolaridad").val();
    $("#selEscolaridad").select2({
        minimumResultsForSearch: -1,
        placeholder: 'Selecciona la escolaridad',
        ajax: {
            url: urlEscolaridad,
            dataType: 'json',
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
        }
    });
}
//-------------------------------------------------------------------
function fnObtenerReligion() {
    var urlReligion = $("#hdnUrlReligion").val();
    $("#selReligion").select2({
        minimumResultsForSearch: -1,
        placeholder: 'Selecciona la religión',
        ajax: {
            url: urlReligion,
            dataType: 'json',
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
        }
    });
}
//-------------------------------------------------------------------
function fnObtenerEstadoCivil() {
    var urlEstadoCivil = $("#hdnUrlEstadoCivil").val();
    $("#selEstadoCivil").select2({
        minimumResultsForSearch: -1,
        placeholder: 'Selecciona el estado civil',
        ajax: {
            url: urlEstadoCivil,
            dataType: 'json',
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
        }
    });
}
//-------------------------------------------------------------------
function fnObtenerTipoSangre() {
    var urlTipoSangre = $("#hdnUrlTipoSangre").val();
    $("#selTipoSangre").select2({
        minimumResultsForSearch: -1,
        placeholder: 'Selecciona el tipo de sangre',
        ajax: {
            url: urlTipoSangre,
            dataType: 'json',
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
        }
    });
}
//-------------------------------------------------------------------
function fnControlesEstado(Estado) {
    switch (Estado) {
        case "Inicio":
            $(".agyo").css("display", "none");
            break;
        case "Mujer":
            $(".agyo").css("display", "block");
            $("#txtGesta").addClass("required");
            $("#txtParto").addClass("required");
            $("#txtAborto").addClass("required");
            $("#txtCesarea").addClass("required");
            $("#txtPapanicolau").addClass("required");
            $("#txtMastografia").addClass("required");
            break;
        case "Hombre":
            $(".agyo").css("display", "none");
            $("#txtGesta").removeClass("required");
            $("#txtParto").removeClass("required");
            $("#txtAborto").removeClass("required");
            $("#txtCesarea").removeClass("required");
            $("#txtPapanicolau").removeClass("required");
            $("#txtMastografia").removeClass("required");
            break;
        case "Editar":
            $(".agyo").css("display", "none");
            fnSelectItem("selSexo", "hdnUrlSexoId", "IdSexo", $("#hdnIdSexo").val());
            fnSelectItem("selEscolaridad", "hdnUrlEscolaridadId", "IdEscolaridad", $("#hdnIdEscolaridad").val());
            fnSelectItem("selReligion", "hdnUrlReligionId", "IdReligion", $("#hdnIdReligion").val());
            fnSelectItem("selEstadoCivil", "hdnUrlEstadoCivilId", "IdEstadoCivil", $("#hdnIdEstadoCivil").val());
            fnSelectItem("selTipoSangre", "hdnUrlTipoSangreId", "IdTipoSangre", $("#hdnIdTipoSangre").val());
            break;
        default:
            break;
    }
}
//-------------------------------------------------------------------
function fnDefinirGrid() {
    var _Columns;

    _Columns = [
        //{ title: 'IdUsuario', field: 'IdUsuario', hidden: true, },
        { title: 'Nombre', field: 'Nombre', /*filterable: { multi: true, dataSource: filterSource },*/ width: 140 },
        { title: 'Descripción', field: 'Descripción', /*filterable: { multi: true, dataSource: filterSource },*/ width: 160 },
        { title: 'Fecha Actualización', field: 'FechaActualizacion', filterable: false, format: "{0:dd/MM/yyyy HH:mm:ss}", width: 140 },
        {
            title: "Archivo",
            field: 'Archivo', /*filterable: { multi: true, dataSource: filterSource },*/
            //template: function (dataItem) {
            //    if (dataItem.HistoriaClinicaActiva == 1) {
            //        return "<input type='checkbox' class='js-switch' checked />"
            //    }
            //    else {
            //        return "<input type='checkbox' class='js-switch'/>"
            //    }
            //},
            width: 120
        },
    ];
    $("#grdEstudiosClinicos").kendoGrid({
        //dataSource: dataSourceHistoriaClinica,
        height: 550,
        //pdf: {
        //    allPages: true,
        //    avoidLinks: true,
        //    paperSize: "A4",
        //    margin: { top: "2cm", left: "1cm", right: "1cm", bottom: "1cm" },
        //    landscape: true,
        //    repeatHeaders: true,
        //    template: $("#page-template").html(),
        //    scale: 0.8
        //},

        //pdf: {
        //    allPages: true,
        //    paperSize: "A4",
        //    landscape: true,
        //    scale: 0.75,
        //    fileName: "Usuarios.pdf"
        //},
        //excel: {
        //    allPages: true,
        //    filterable: true,
        //    fileName: "Pacientes.xlsx"
        //},
        //excelExport: function (e) {
        //    var sheet = e.workbook.sheets[0];
        //    for (var rowIndex = 1; rowIndex < sheet.rows.length; rowIndex++) {
        //        var row = sheet.rows[rowIndex];
        //        for (var cellIndex = 0; cellIndex < row.cells.length; cellIndex++) {

        //            if (cellIndex > 10 && cellIndex < 19) {
        //                row.cells[cellIndex].format = "#,##0.00";
        //            }
        //            else if (cellIndex == 0 && cellIndex == 10) {
        //                row.cells[cellIndex].format = "dd/MM/yyyy";
        //            }

        //        }
        //    }
        //},
        //detailInit: detailInitPerfilUsuario,
        //groupable: {
        //    messages: {
        //        empty: "Arrastre un encabezado de columna y suéltelo aquí para agrupar por esa columna"
        //    }
        //},
        reorderable: true,
        scrollable: true,
        sortable: true,
        resizable: true,
        pageable: {
            messages: {
                display: "{0} - {1} de {2} Registros", //{0} is the index of the first record on the page, {1} - index of the last record on the page, {2} is the total amount of records
                empty: "No hay elementos para mostrar",
                page: "Página",
                allPages: "Todas",
                of: "de {0}", //{0} is total amount of pages
                itemsPerPage: "Registros por página",
                first: "Ir a la primera página",
                previous: "Ir a la página anterior",
                next: "Ir a la página siguiente",
                last: "Ir a la última página",
                refresh: "Actualizar"
            },
            refresh: false,
            pageSizes: true,
            buttonCount: 5
        },
        filterable: true,
        //filterMenuInit: function (e) {
        //    var grid = e.sender;
        //    e.container.data("kendoPopup").bind("open", function () {
        //        filterSource.sort({ field: e.field, dir: "asc" });
        //        var uniqueDsResult = removeDuplicates(grid.dataSource.view(), e.field);
        //        filterSource.data(uniqueDsResult);
        //    })
        //},
        columns: _Columns,
        //dataBound: function (e) {
        //    var elem = document.querySelector('.js-switch');
        //    var switchery = new Switchery(elem, { color: '#1AB394' });
        //    var rows = e.sender.tbody.children();
        //    for (var j = 0; j < rows.length; j++) {
        //        var row = $(rows[j]);
        //        var dataItem = e.sender.dataItem(row);
        //        if (dataItem.HistoriaClinicaActiva == 1) {
        //            switchery.disable();
        //        }
        //        //Cuando se active el check se manda al form de historia clínica
        //        elem.onchange = function () {
        //            fnHistoriaClinica(dataItem.IdUsuario);
        //        };
        //    }
        //}
    });
}
//-------------------------------------------------------------------
function fnSelectItem(NombreSelect, NombreHidden, NombreId, Id) {
    var _Select = $("#" + NombreSelect + "");
    $.ajax({
        url: $("#" + NombreHidden + "").val() + "?" + NombreId + "=" + Id,
        dataType: 'json'
    }).then(function (data) {
        var option = new Option(data.items[0].text, data.items[0].id, true, true);
        _Select.append(option).trigger('change');

        _Select.trigger({
            type: 'select2:select',
            params: {
                data: data.items[0]
            }
        });
    });
}