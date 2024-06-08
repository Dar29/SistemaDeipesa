"use strict";

var datatable;

const controllerName = window.location.pathname.split("/")[1];

$(() => {
  $("#txtBuscar").on("change", (e) => {
    const texto = $(e.target).val();
    datatable.search(texto).draw();
  });

  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: `/${controllerName}/ObtenerTodos`,
      type: "GET",
      dataType: "json",
      dataSrc: "",
    },
    columnDefs: [
      {
        targets: 2,
        render: (data) => moment(data).format("DD/MM/YYYY"),
      },
    ],
    columns: [
      { data: "IdFactura" },
      { data: "NombreCliente" },
      { data: "FechaEmision" },
      { data: "Impuesto" },
      { data: "Descuento" },
      { data: "Total" },
      {
        data: "Activo",
        render: (valor) =>
          valor
            ? '<span class="badge bg-success"><i class="fas fa-check"></i></span>'
            : '<span class="badge bg-danger"><i class="fas fa-xmark"></i></span>',
      },
      {
        defaultContent:
          '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar"><i class="fas fa-trash"></i></button>',
        orderable: false,
        searchable: false,
        width: "40px",
      },
    ],
    language: {
      decimal: "",
      emptyTable: "No hay información",
      info: "Mostrando _START_ a _END_ de _TOTAL_ Registros",
      infoEmpty: "Mostrando 0 to 0 of 0 Registros",
      infoFiltered: "(Filtrado de _MAX_ total registros)",
      infoPostFix: "",
      thousands: ",",
      lengthMenu: "Mostrar _MENU_ Registros",
      loadingRecords: "Cargando...",
      processing: "Procesando...",
      search: "Buscar:",
      zeroRecords: "Sin resultados encontrados",
      paginate: {
        first: "Primero",
        last: "Ultimo",
        next: "Siguiente",
        previous: "Anterior",
      },
    },
  });

  $("#btnCrearRegistro").on(
    "click",
    () => (window.location.href = `${controllerName}/Nueva`)
  );

  $("#tabla tbody").on("click", ".btn-eliminar", function () {
    console.log("asdfasdf");
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    $.ajax({
      url: `/${controllerName}/Anular`,
      type: "POST",
      data: { id: data.IdFactura },
      success: ({ Exitoso, Mensaje }) => {
        if (Exitoso) return datatable.ajax.reload();
        alert(Mensaje);
      },
    });
  });

  // $("#tabla tbody").on("click", ".btn-editar", function () {
  //   var filaSeleccionada = $(this).closest("tr");

  //   var data = datatable.row(filaSeleccionada).data();

  //   abrirModal(data);
  // });
});
