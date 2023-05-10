$(document).ready(function () {
    $("#btnBorrar").click(function () {
        var datasource = $("#grdHistoriaClinica").data("kendoGrid").dataSource;
        datasource.filter([]);
    });

    $("#btnBorrarResponsive").click(function () {
        var datasource = $("#grdHistoriaClinica").data("kendoGrid").dataSource;
        datasource.filter([]);
    });


    $("#btnExcel").click(function () {
        $('.ibox').children('.ibox-content').toggleClass('sk-loading');
        $('#grdHistoriaClinica').data('kendoGrid').saveAsExcel();
        setTimeout(function () { $('.ibox').children('.ibox-content').removeClass('sk-loading'); }, 3000);
        return false;
    });

    $("#btnExcelResponsive").click(function () {
        $('.ibox').children('.ibox-content').toggleClass('sk-loading');
        $('#grdHistoriaClinica').data('kendoGrid').saveAsExcel();
        setTimeout(function () { $('.ibox').children('.ibox-content').removeClass('sk-loading'); }, 3000);
        return false;
    });

    fnObtenerHistoriaClinicaPacientes();
});
//-------------------------------------------------------------------
function fnObtenerHistoriaClinicaPacientes() {

    var dataSourceHistoriaClinica = new kendo.data.DataSource({
        transport: {
            read: {
                url: "ConsultaHistoriaClinicaPacientes",
                dataType: "jsonp",
            },
        },
        sort: {
            field: "Nombre",
            dir: "desc"
        },
        schema: {
            model: {

                fields: {
                    IdUsuario: { type: "string" },
                    Correo: { type: "string" },
                    Nombre: { type: "string" },
                    ApellidoPaterno: { type: "string" },
                    ApellidoMaterno: { type: "string" },
                    Curp: { type: "string" },
                    IdEstatusUsuario: { type: "number" },
                    FechaActualizacion: { type: "date" },
                    HistoriaClinicaActiva: { type: "number" },
                }
            }
        },
        pageSize: 10,
        change: function (e) {
            filterSource.data(e.items);
        },
    });

    var filterSource = new kendo.data.DataSource({
        data: dataSourceHistoriaClinica
    });

    var _Columns;

    _Columns = [
        { title: 'IdUsuario', field: 'IdUsuario', hidden: true, },
        { title: 'Nombre', field: 'Nombre', filterable: { multi: true, dataSource: filterSource }, width: 140 },
        { title: 'Apellido Paterno', field: 'ApellidoPaterno', filterable: { multi: true, dataSource: filterSource }, width: 160 },
        { title: 'Apellido Materno', field: 'ApellidoMaterno', filterable: { multi: true, dataSource: filterSource }, width: 160 },
        { title: 'Curp', field: 'Curp', filterable: false, width: 170 },
        { title: 'Fecha Actualización', field: 'FechaActualizacion', filterable: false, format: "{0:dd/MM/yyyy HH:mm:ss}", width: 140 },
        {
            title: "Historía Clínica",
            template: function (dataItem) {
                if (dataItem.HistoriaClinicaActiva == 1) {
                    return "<input type='checkbox'class='js-switch-checked' checked/>"
                }
                else {
                    return "<input type='checkbox' class='js-switch'/>"
                }
            },
            width: 120
        },
        {
            title: "Edición",
            template: function (dataItem) {
                if (dataItem.HistoriaClinicaActiva == 1) {
                    return "<a class='editar btn btn-success btn-outline btn-circle' onclick='fnHistoriaClinica(\"" + dataItem.IdUsuario + " \")'><i class='fa fa-pencil'></i></a>"
                }
                else {
                    return "<a class='editar btn btn-white btn-bitbucket btn-circle disabled'><i class='fa fa-pencil'></i></a>"
                }
                //return "<a class='editar btn btn-success btn-outline btn-circle' onclick='fnEditar(\"" + dataItem.IdUsuario + " \")' ><i class='fa fa-pencil'></i></a>"
            },
            width: 70
        },

        {
            title: "Consulta",
            template: function (dataItem) {
                //return "<a class='eliminar btn btn-success btn-outline btn-circle' onclick='fnEliminar(\"" + dataItem.IdUsuario + " \")' ><i class='fa fa-times'></i></a>"
                if (dataItem.HistoriaClinicaActiva == 1) {
                    return "<a class='eliminar btn btn-success btn-outline btn-circle'><i class='fa fa-file-text'></i></a>"
                }
                else {
                    return "<a class='eliminar btn btn-white btn-bitbucket btn-circle disabled'><i class='fa fa-file-text'></i></a>"
                }
            },
            width: 90
        },
    ];


    $("#grdHistoriaClinica").kendoGrid({
        dataSource: dataSourceHistoriaClinica,
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
        excel: {
            allPages: true,
            filterable: true,
            fileName: "Pacientes.xlsx"
        },
        excelExport: function (e) {
            var sheet = e.workbook.sheets[0];
            for (var rowIndex = 1; rowIndex < sheet.rows.length; rowIndex++) {
                var row = sheet.rows[rowIndex];
                for (var cellIndex = 0; cellIndex < row.cells.length; cellIndex++) {

                    if (cellIndex > 10 && cellIndex < 19) {
                        row.cells[cellIndex].format = "#,##0.00";
                    }
                    else if (cellIndex == 0 && cellIndex == 10) {
                        row.cells[cellIndex].format = "dd/MM/yyyy";
                    }

                }
            }
        },
        //detailInit: detailInitPerfilUsuario,
        groupable: {
            messages: {
                empty: "Arrastre un encabezado de columna y suéltelo aquí para agrupar por esa columna"
            }
        },
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
        filterMenuInit: function (e) {
            var grid = e.sender;
            e.container.data("kendoPopup").bind("open", function () {
                filterSource.sort({ field: e.field, dir: "asc" });
                var uniqueDsResult = removeDuplicates(grid.dataSource.view(), e.field);
                filterSource.data(uniqueDsResult);
            })
        },
        columns: _Columns,
    });
    var grid = $("#grdHistoriaClinica").data("kendoGrid");
    grid.bind("dataBound", grid_dataBound);
    grid.dataSource.fetch();
    //$("#grdHistoriaClinica").data("kendoGrid").wrapper.find(".k-grid-header-wrap").off("scroll.kendoGrid");
}
//-------------------------------------------------------------------
function removeDuplicates(items, field) {
    var getter = function (item) { return item[field] },
        result = [],
        index = 0,
        seen = {};

    while (index < items.length) {
        var item = items[index++],
            text = getter(item);

        if (text !== undefined && text !== null && !seen.hasOwnProperty(text)) {
            result.push(item);
            seen[text] = true;
        }
    }
    return result;
}
//-------------------------------------------------------------------
function fnHistoriaClinica(IdUsuario) {
    window.location.href = $("#hdnUrlHistoriaClinica").val() + "?IdUsuario=" + IdUsuario;
    //alert($("#hdnUrlHistoriaClinica").val() + "?IdUsuario =" + IdUsuario);
}
//-------------------------------------------------------------------
//function grid_dataBound(e) {
//    //var gridRows = this.tbody.find("tr");
//    //gridRows.each(function (e) {
//    //    var elem = document.querySelector('.js-switch');
//    //    var switchery = new Switchery(elem, { color: '#1AB394' });

//    //    //custom logic
//    //});
//    //var grid = this;
//    //var dataSource = grid.dataSource;
//    ////Loop through each record in a Kendo Grid
//    //$.each(grid.items(), function (index, item) {
//    //    var uid = $(item).data("uid");
//    //    var dataItem = dataSource.getByUid(uid);
//    //    //Add an ID to each row as an example
//    //    //$(item).attr("id", dataItem.SomeField + "-"
//    //    //    + dataItem.AnotherField);
//    //    alert(dataItem.Nombre);
//    //    var elem = document.querySelector('.js-switch');
//    //    var switchery = new Switchery(elem, { color: '#1AB394' });
//    //});

//    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));

//    elems.forEach(function (html) {
//        var switchery = new Switchery(html, { color: '#1AB394' });
//    });

//    //var rows = e.sender.tbody.children();


//    //for (var j = 0; j < rows.length; j++) {
//    //    var row = $(rows[j]);
//    //    var dataItem = e.sender.dataItem(row);
//    //    if (dataItem.HistoriaClinicaActiva == 1) {

//    //    }
//    //}
//    //var grid = $("#grdHistoriaClinica").data("kendoGrid");
//    //var _data = grid.dataSource.data();
//    //for (var i = 0; i < _data.length; i++) {
//    //    if (_data[i].HistoriaClinicaActiva == 1) {
//    //        alert("Inactivo");

//    //    }
//    //    else {
//    //        alert("Activo");

//    //        //var switchery = new Switchery(elem, { disabled: false });
//    //    }
//        //alert(_data[i].HistoriaClinicaActiva); // displays "name" and then "age"
//    //}    
//}
//-------------------------------------------------------------------
function grid_dataBound() {
    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
    var elemsChecked = Array.prototype.slice.call(document.querySelectorAll('.js-switch-checked'));
    var data = this.dataSource.view();
    var intIndex = 0;
    var IdUsuarios = [];
    for (var i = 0; i < data.length; i++) {
        var dataItem = data[i];
        if (dataItem.HistoriaClinicaActiva == 0) {
            IdUsuarios[intIndex] = dataItem.IdUsuario;
            intIndex++;
        }
    }
    intIndex = 0;
    elems.forEach(function (html) {
        var switchery = new Switchery(html, { color: '#1AB394' });
        html.onchange = function () {
            //fnHistoriaClinica(IdUsuarios[intIndex]);
            for (var j = 0; j < IdUsuarios.length; j++) {
                if (intIndex == j) {
                    alert(IdUsuarios[j]);
                }
            }

        };

        intIndex++;
    });

    elemsChecked.forEach(function (html) {
        var switcheryChecked = new Switchery(html, { color: '#1AB394' });
        switcheryChecked.disable();
    });

    //var HistoriaClinicaNueva = document.querySelectorAll('.js-switch');

    //HistoriaClinicaNueva.onchange = function () {
    //    alert(HistoriaClinicaNueva.checked)
    //};
    //for (var i = 0; i < data.length; i++) {
    //    var dataItem = data[i];

    //    elems.onchange = function () {
    //        fnHistoriaClinica(dataItem.IdUsuario);
    //    };
    //    //var tr = $("#grdHistoriaClinica").find("[data-uid='" + dataItem.uid + "']");
    //    // use the table row (tr) and data item (dataItem)
    //    //if (!Pacientes.includes(dataItem.Nombre)) {
    //    //    Pacientes[i] = dataItem.Nombre;
    //    //    if (dataItem.HistoriaClinicaActiva == 1) {
    //    //        var elem = document.querySelector('.js-switch-checked');
    //    //        var switchery = new Switchery(elem, { color: '#1AB394' });
    //    //        switchery.disable();
    //    //    }
    //    //    else {
    //    //        var elem = document.querySelector('.js-switch');
    //    //        var switchery = new Switchery(elem, { color: '#1AB394' });
    //    //        //Cuando se active el check se manda al form de historia clínica
    //    //        elem.onchange = function () {
    //    //            fnHistoriaClinica(dataItem.IdUsuario);
    //    //        };
    //    //    }
    //    //}
    //    //if (dataItem.HistoriaClinicaActiva == 1) {
    //    //    //var elem = document.querySelector('.js-switch-checked');
    //    //    //var switchery = new Switchery(elem, { color: '#1AB394' });
    //    //    switcheryChecked.disable();
    //    //}
    //    //else {
    //    //    //var elem = document.querySelector('.js-switch');
    //    //    //var switchery = new Switchery(elem, { color: '#1AB394' });
    //    //    ////Cuando se active el check se manda al form de historia clínica
    //    //    elems.onchange = function () {
    //    //        fnHistoriaClinica(dataItem.IdUsuario);
    //    //    };
    //    //}
    //}
    //intIndex++;
}
