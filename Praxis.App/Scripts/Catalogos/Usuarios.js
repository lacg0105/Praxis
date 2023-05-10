$(document).ready(function () {
    fnObtenerUsuarios();
    Exportaciones();

    $("#btnLimpiarFiltros").click(function () {
        var datasource = $("#grdUsers").data("kendoGrid").dataSource;
        datasource.filter([]);
    });
    

});

//-------------------------------------------------------------------
function fnObtenerUsuarios() {
    var dataSourceUsers = new kendo.data.DataSource({
        transport: {
            read: {
                url: "ObtenerUsuarios",
                dataType: "jsonp",
            },
        },
        sort: {
            field: "FechaCreacion",
            dir: "desc"
        },
        schema: {
            model: {

                fields: {
                    IdUsuario: { type: "string" },
                    Nombre: { type: "string" },
                    Pwd: { type: "string" },
                    Correo: { type: "string" },
                    FechaCreacion: { type: "date" },
                    IdEstatusUsuario: { type: "number" },
                    NombreEstatus: { type: "string" },
                    IdRol: { type: "number" },
                    NombreRol: { type: "string" },
                    IdArea: { type: "number" },
                    NombreArea: { type: "string" },
                    NombreImagen: { type: "string" },
                    NombreInstitucion: { type: "string" },
                    FechaAcceso: { type: "date" }
                }
            }
        },
        pageSize: 10,
    });

    var _Columns;
    //if (Access == 1) {
    //    _Columns = [
    //        {
    //            template: "<div class='customer-photo'" +
    //                "style='background-image: url(../Content/Usuarios/#:data.Img#), url(../Content/Img/Icono%20Empresario.png);'></div>"
    //                + "<div class='customer-name'>#: Nombre #</div>",
    //            title: " ",
    //            field: "Nombre",

    //            width: 120
    //        },
    //        // { title: 'Nombre', field: 'Nombre', width: 100, },
    //        { title: 'Correo', field: 'Correo', width: 100, },
    //        { title: 'Rol', field: 'NombreRol', width: 70, align: "right", },
    //        { title: 'Institución', field: 'NombreInstitucion', width: 70, align: "right", },
    //        { title: 'Área', field: 'NombreArea', width: 70, align: "right", },
    //        { title: 'Estatus', field: 'NombreEstatus', width: 100, },
    //        { title: 'Creado', field: 'FechaCreacion', width: 70, format: "{0:dd/MM/yyyy}" },
    //        {
    //            title: " ",
    //            width: 30,
    //            template: function (dataItem) {
    //                return "<a class='editar btn btn-success btn-outline btn-circle' onclick='fnEditar(\"" + dataItem.Id + " \")' ><i class='fa fa-edit'></i></a>"
    //            }
    //        },

    //        {
    //            title: " ",
    //            width: 30,
    //            template: function (dataItem) {
    //                return "<a class='eliminar btn btn-success btn-outline btn-circle' onclick='fnEliminar(\"" + dataItem.Id + " \")' ><i class='fa fa-times'></i></a>"
    //            }
    //        },
    //        { title: 'IdUser', field: 'Id', hidden: true, },
    //        { title: 'Img', field: 'Img', hidden: true, },

    //        { title: 'IdStatus', field: 'IdEstatus', hidden: true },
    //        { title: 'IdRol', field: 'IdRol', hidden: true },
    //        { title: 'IdArea', field: 'IdArea', hidden: true },
    //        { title: 'IdFinancialInstitution', field: 'IdInstitucionFinanciera', hidden: true },

    //    ];
    //}
    //else {
    //    _Columns = [
    //        {
    //            template: "<div class='customer-photo'" +
    //                "style='background-image: url(../Content/Usuarios/#:data.Img#), url(../Content/Img/Icono%20Empresario.png);'></div>"
    //                + "<div class='customer-name'>#: Nombre #</div>",
    //            title: " ",
    //            field: "Nombre",

    //            width: 120
    //        },
    //        //{ title: 'Nombre', field: 'Nombre', width: 100, },
    //        { title: 'Correo', field: 'Correo', width: 100, },
    //        { title: 'Rol', field: 'NombreRol', width: 70, align: "right", },
    //        { title: 'Institución', field: 'NombreInstitucion', width: 70, align: "right", },
    //        { title: 'Área', field: 'NombreArea', width: 70, align: "right", },
    //        { title: 'Estatus', field: 'NombreEstatus', width: 100, },
    //        { title: 'Creado', field: 'FechaCreacion', width: 70, format: "{0:dd/MM/yyyy}" },
    //        { title: 'IdUser', field: 'Id', hidden: true, },
    //        { title: 'Img', field: 'Img', hidden: true, },
    //        { title: 'IdStatus', field: 'IdEstatus', hidden: true },
    //        { title: 'IdRol', field: 'IdRol', hidden: true },
    //        { title: 'IdArea', field: 'IdArea', hidden: true },
    //        { title: 'IdFinancialInstitution', field: 'IdInstitucionFinanciera', hidden: true },


    //    ];
    //}

    _Columns = [
        {
            template: "<div class='customer-photo'" +
                "style='background-image: url(/Content/Img/Usuarios/#:data.NombreImagen#);'></div>"
                + "<div class='customer-name'>#: Nombre #</div>",
            title: " ",
            field: "Nombre",
            filterable: false
            
        },
        // { title: 'Nombre', field: 'Nombre', width: 100, },
        { title: 'Correo', field: 'Correo', filterable: false},
        { title: 'Rol', field: 'NombreRol', align: "right", filterable: { ui: RolFilter } },
        { title: 'Área', field: 'NombreArea', align: "right", filterable: false},
        { title: 'Institución', field: 'NombreInstitucion', align: "right", filterable: { ui: InstitucionFilter } },
        { title: 'Estatus', field: 'NombreEstatus', filterable: { ui: EstatusFilter } },
        { title: 'Creado', field: 'FechaCreacion', format: "{0:dd/MM/yyyy}", filterable: false },
        { title: 'Fecha \nAcceso', field: 'FechaAcceso', format: "{0:dd/MM/yyyy}", filterable: false },
        {
            title: " ",
            template: function (dataItem) {
                return "<a class='editar btn btn-success btn-outline btn-circle' onclick='fnEditar(\"" + dataItem.IdUsuario + " \")' ><i class='fa fa-edit'></i></a>"
            },
            width: 50
        },

        {
            title: " ",
            template: function (dataItem) {
                return "<a class='eliminar btn btn-success btn-outline btn-circle' onclick='fnEliminar(\"" + dataItem.IdUsuario + " \")' ><i class='fa fa-times'></i></a>"
            },
            width: 50
        },
        { title: 'IdUser', field: 'IdUsuario', hidden: true, },
        { title: 'Img', field: 'NombreImagen', hidden: true, },

        { title: 'IdEstatus', field: 'IdEstatusUsuario', hidden: true },
        { title: 'IdRol', field: 'IdRol', hidden: true },
        { title: 'IdArea', field: 'IdArea', hidden: true },
    ];
    
   
    $("#grdUsers").kendoGrid({
        dataSource: dataSourceUsers,
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

        pdf: {
            allPages: true,
            paperSize: "A4",
            landscape: true,
            scale: 0.75,
            fileName: "Usuarios.pdf"
        },
        excel: {
            allPages: true,
            filterable: true,
            fileName: "Usuarios.xlsx"
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
        detailInit: detailInitPerfilUsuario,
        groupable: {
            messages: {
                empty: "Arrastre un encabezado de columna y suéltelo aquí para agrupar por esa columna"
            }
        },
        reorderable: true,
        //scrollable: false,
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
        filterable: {
            messages: {
                info: "Criterios del filtro:", // sets the text on top of the Filter menu
                filter: "Filtrar", // sets the text for the "Filter" button
                clear: "Borrar", // sets the text for the "Clear" button

                // when filtering boolean numbers
                isTrue: "verdadero", // sets the text for "isTrue" radio button
                isFalse: "falso", // sets the text for "isFalse" radio button

                //changes the text of the "And" and "Or" of the Filter menu
                and: "y",
                or: "ó"
            },
            operators: {
                //filter menu for "string" type columns
                string: {
                    eq: "Es igual a",
                    neq: "Es diferente a",
                    startswith: "Empieza con",
                    contains: "Contiene",
                    endswith: "Termina con"
                },
                //filter menu for "number" type columns
                number: {
                    eq: "Es igual a",
                    neq: "Es diferente a",
                    gte: "Es mayor o igual a",
                    gt: "Es mayor que",
                    lte: "Es menor o igual que",
                    lt: "Es menos que"
                },
                //filter menu for "date" type columns
                date: {
                    eq: "Es igual a",
                    neq: "Es diferente a",
                    gte: "Es después o igual a",
                    gt: "Es después",
                    lte: "Es anterior o igual a",
                    lt: "Es antes"
                },
                //filter menu for foreign key values
                enums: {
                    eq: "Es igual a",
                    neq: "Es diferente a"
                }
            }
        },

        columns: _Columns,



    });

    $("#grdUsers").data("kendoGrid").wrapper.find(".k-grid-header-wrap").off("scroll.kendoGrid");
}
//-------------------------------------------------------------------
function fnEditar(idUsuario) {
    window.location.href = $("#hdnUrlEditUser").val() + "?idUsuario=" + idUsuario;
}
//-------------------------------------------------------------------
function RolFilter(element) {
    var remoteDataSource = new kendo.data.DataSource({
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
    });
    element.kendoDropDownList({
        dataSource: remoteDataSource,
        dataTextField: "NombreRol",
        dataValueField: "NombreRol",
        optionLabel: "--Rol--"
    });
}
//-------------------------------------------------------------------
function InstitucionFilter(element) {
    var remoteDataSource = new kendo.data.DataSource({
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
    });
    element.kendoDropDownList({
        dataSource: remoteDataSource,
        dataTextField: "NombreInstitucion",
        dataValueField: "NombreInstitucion",
        optionLabel: "--Institución--"
    });
}
//-------------------------------------------------------------------
function EstatusFilter(element) {
    var remoteDataSource = new kendo.data.DataSource({
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
    });
    element.kendoDropDownList({
        dataSource: remoteDataSource,
        dataTextField: "NombreEstatus",
        dataValueField: "NombreEstatus",
        optionLabel: "--Estatus--"
    });
}
//-------------------------------------------------------------------
function Exportaciones() {
    $("#btnExpExcelgrd").click(function () {
        $('#grdUsers').data('kendoGrid').saveAsExcel();
        return false;
    });

    $("#btnExpPdfgrd").click(function () {
        $('#grdUsers').data('kendoGrid').saveAsPDF();
        return false;
    });
}
//-------------------------------------------------------------------
function fnEliminar(idUsuario) {

    swal({
        title: "¿Desea continuar?",
        text: "El usuario se borrara de manera permanente",
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

                $('.ibox').children('.ibox-content').toggleClass('sk-loading');

                var datToSend = "{'IdUsuario': '" + idUsuario + "'}";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: "EliminarUsuario",
                    data: datToSend,
                    success: function (operationResult) {
                        $('.ibox').children('.ibox-content').removeClass('sk-loading');
                        if (operationResult.IsSuccess == 1) {
                            swal("Proceso completado con éxito", operationResult.Message, "success");
                        }
                        else {
                            swal("Validación", operationResult.Message, "error");
                        }
                        $('#grdUsers').data('kendoGrid').dataSource.read();
                        $('#grdUsers').data('kendoGrid').refresh();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        toastr.error(thrownError, 'Error en el proceso');
                        $('.ibox').children('.ibox-content').removeClass('sk-loading');
                    }
                });


            }
        });


}
//-------------------------------------------------------------------
function detailInitPerfilUsuario(e) {
    // Initialize a new jQuery Deferred http://api.jquery.com/jQuery.Deferred/
    //var deferred = $.Deferred();

    //// Get the index of the master row.
    //var masterRowIndex = e.masterRow.index(".k-master-row");

    //// Add the deferred to the list of promises.
    //detailExportPromises.push(deferred);
    if (e.data.IdRol == 2) {
        var _IdUsuario = e.data.IdUsuario;
        var urlPerfilUsuario = $("#hdnUrlObtenerPerfilUsuario").val() + "?IdUsuario=" + _IdUsuario;
        var dataPerfilUsuario = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urlPerfilUsuario,
                    dataType: "jsonp",
                },
            },
            schema: {
                model: {
                    fields: {
                        IdUsuario: { type: "string" },
                        FechaNacimiento: { type: "date" },
                        Rfc: { type: "string" },
                        Profesion: { type: "string" },
                        Cedula: { type: "string" },
                        Consultorio: { type: "string" },
                        Direccion: { type: "string" },
                        AlcaldiaMunicipio: { type: "string" },
                        Estado: { type: "string" },
                        CodigoPostal: { type: "string" },
                        CostoConsulta: { type: "number" },
                        ConsultaDomicilio: { type: "boolean" },
                        ConsultaVideollamada: { type: "boolean" }
                    }
                }
            },

        });

        var grid = $("<div/>").appendTo(e.detailCell).kendoGrid({
            dataSource: dataPerfilUsuario,
            scrollable: true,
            sortable: false,
            filterable: false,
            pageable: false,
            resizable: true,
            headerAttributes: {
                style: "display: none"
            },
            //excelExport: function (e) {
            //    // Prevent the saving of the file.
            //    e.preventDefault();

            //    // Resolve the deferred.
            //    deferred.resolve({
            //        masterRowIndex: masterRowIndex,
            //        sheet: e.workbook.sheets[0]
            //    });
            //},
            columns: [
                {
                    title: 'Fecha Nacimiento', field: 'FechaNacimiento', width: 120, format: "{0:dd/MM/yyyy}"
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'RFC', field: 'Rfc', width: 140, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },

                {
                    title: 'Profesión', field: 'Profesion', width: 90, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Cédula', field: 'Cedula', width: 250, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Consultorio', field: 'Consultorio', width: 250, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Dirección', field: 'Direccion', width: 90, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Alcaldía / Municipio', field: 'AlcaldiaMunicipio', width: 120, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Estado', field: 'Estado', width: 90, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Código Postal', field: 'CodigoPostal', width: 90, locked: false, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Costo Consulta', field: 'CostoConsulta', width: 120, format: "{0:#,##0}", locked: false, filterable: false,
                    //headerAttributes: { "class": "table-header-cell", style: "text-align: center; vertical-align: middle;white-space: normal !important;" },
                },

                {
                    title: 'Consulta Domicilio', field: 'ConsultaDomicilio', width: 120, filterable: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
                {
                    title: 'Consulta Videollamada', field: 'ConsultaVideollamada', width: 120, filterable: false, locked: false,
                    //headerAttributes: {
                    //    "class": "table-header-cell",
                    //    style: "text-align: center; vertical-align: middle;white-space: normal !important;"
                    //},
                },
            ]
        });

        $(grid).find(".k-grid-header").css('display', 'block');
    }
}
//-------------------------------------------------------------------