"use strict";

var datatable;

$(() => {
  $("#txtBuscar").on("change", (e) => {
    const texto = $(e.target).val();
    datatable.search(texto).draw()
  });
  
  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Compras/ObtenerTodos",
      type: "GET",
      dataType: "json",
      dataSrc: ""
    },
    columnDefs: [
      {
        targets: 2,
        render: (data) => moment(data).format("DD/MM/YYYY"),
      },
    ],
    columns: [
      { data: "IdCompra" },
      { data: "NombreProveedor" },
      { data: "FechaEmision" },
      { data: "Impuesto" },
      { data: "Descuento" },
      { data: "Total" },
      {
        data: "Estado",
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
        width: "90px",
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

  $("#btnCrearRegistro").on("click", () => window.location.href = "Compras/Nueva");

  // $("#tabla tbody").on("click", ".btn-eliminar", function () {
  //   var filaSeleccionada = $(this).closest("tr");

  //   var data = datatable.row(filaSeleccionada).data();

  //   $.ajax({
  //     url: `/Categorias/Desactivar`,
  //     type: "POST",
  //     data: { id: data.IdCategoria },
  //     success: () => datatable.ajax.reload(),
  //   });
  // });

  // $("#tabla tbody").on("click", ".btn-editar", function () {
  //   var filaSeleccionada = $(this).closest("tr");

  //   var data = datatable.row(filaSeleccionada).data();

  //   abrirModal(data);
  // });
});

