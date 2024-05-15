"use strict";

var datatable;

$(() => {
  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Home/ListaCategorias",
      type: "GET",
      dataType: "json",
    },
    columns: [
      { data: "IdCategoria" },
      { data: "Descripcion" },
      {
        data: "Estado",
        render: (valor) =>
          valor
            ? '<span class="badge bg-success"><i class="fas fa-check"></i></span>'
            : '<span class="badge bg-danger"><i class="fas fa-xmark"></i></span>',
      },
      {
        defaultContent:
          '<button type="button" class="btn btn-primary btn-sm btn-editar"><i class="fas fa-pen"></i></button>' +
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

  $("#btnCrearRegistro").on("click", () => abrirModal());

  $("#tabla tbody").on("click", ".btn-eliminar", function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    $.ajax({
      url: `/Categorias/Desactivar`,
      type: "POST",
      data: { id: data.IdCategoria },
      success: () => datatable.ajax.reload(),
    });
  });

  $("#tabla tbody").on("click", ".btn-editar", function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    abrirModal(data);
  });

  validarFormulario();
});

function validarFormulario() {
  $("form[name='frmCategoria']").validate({
    rules: {
      Descripcion: { required: true },
    },
    messages: {
      Descripcion: {
        required: "Debe proporcionar una descripcion del material",
      },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      $.ajax({
        url: "/Categorias/Guardar",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: ({ Exitoso, Mensaje }) => {
          if (!Exitoso) return alert(Mensaje);

          $("#FormModal").modal("hide");
          datatable.ajax.reload();
        },
      });
    },
  });
}

function abrirModal(json) {
  $("#txtid").val("");
  $("#txtdescripcion").val("");
  $("#selectactivo").val("");

  if (json != null) {
    $("#txtid").val(json.IdCategoria);
    $("#txtdescripcion").val(json.Descripcion);
    $("#selectactivo").val(json.Estado == true ? 1 : 0);
  }

  $("#FormModal").modal("show");
}
