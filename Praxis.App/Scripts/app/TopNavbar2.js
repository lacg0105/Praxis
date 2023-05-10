$(document).ready(function () {
    var url = $("#hdnUrlMenu").val();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: url,
        async: false,
        //data: "{ 'idUser': '" + localStorage.getItem("SCARUsr") + "' }",
        success: function (data) {
            $("#dvTopNavbar2").html(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
            console.log(thrownError);
        }
    });
});
//-------------------------------------------------------------------