$(document).ready(function () {

    $("#btnGuardar").click(function () {
        fnIngresar();
        return false;
    });

    var container = $("#frmLoginForm");
    kendo.init(container);
    container.kendoValidator({
        rules: {
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
            matches: function (input) {
                return input.data("matchesMsg");
            },
            required: "Favor de llenar este campo.",
        }
    });
});
//-----------------------------------------------------
function fnIngresar() {
    var validator = $("#frmLoginForm").data("kendoValidator");
    if (validator.validate()) {

        $('.ibox-content').toggleClass('sk-loading');
        var datToSend = $("#frmLoginForm").serialize();
        $.ajax({
            method: "POST",
            url: "../CambiarPwd",
            //url: "CambiarPwd",
            async: false,
            data: datToSend,
            success: function (operationResult) {
                $('.ibox-content').removeClass('sk-loading');
                if (operationResult.IsSuccess == 1) {
                    //swal("Validación", operationResult.Message, "success");
                    //localStorage.SCARUsr = operationResult.Message;
                    localStorage.IdTemp = 1;
                    window.location.href = $("#urlRedirect").val();
                }
                else {
                    //localStorage.clear();
                    swal("Validación", operationResult.Message, "error");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //localStorage.clear();
                toastr.error(thrownError, 'Error en el proceso');
                $('.ibox-content').removeClass('sk-loading');
            }
        });

    }
}
//-----------------------------------------------------