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
      url: "/Proveedores/Obtener",
      type: "GET",
      dataType: "json",
      dataSrc: "",
    },
    columns: [
      { data: "IdProveedor" },
      { data: "Nombre" },
      { data: "Email" },
      { data: "Telefono" },
      { data: "Direccion" },
      {
        data: "Estado",
        render: (valor) =>
          valor == 1
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
      url: `/Materiales/Desactivar`,
      type: "POST",
      data: { id: data.IdProveedor },
      success: () => datatable.ajax.reload(),
    });
  });

  $("#tabla tbody").on("click", ".btn-editar", async function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    const material = await $.get(
      `/Proveedores/ObtenerPorId?id=${data.IdProveedor}`
    );

    abrirModal(material);
  });

  validarFormulario();
});

function abrirModal(data) {
  $("#txtIdProveedor").val("");
  $("#txtNombre").val("");
  $("#txtEmail").val("");
  $("#txtTelefono").val("");
  $("#txtDireccion").val("");
  $("#selectactivo").val("");

  if (data) {
    $("#txtIdProveedor").val(data.IdProveedor);
    $("#txtNombre").val(data.Nombre);
    $("#txtEmail").val(data.Email);
    $("#txtTelefono").val(data.Telefono);
    $("#txtDireccion").val(data.Direccion);
    $("#selectactivo")
      .val(data.Estado ? 1 : 0)
      .trigger("change");
  }

  $("#FormModal").modal("show");
}

function validarFormulario() {
  $("form[name='frmMaterial']").validate({
    rules: {
      Nombre: { required: true },
      Email: { required: true },
      Telefono: { required: true },
      Direccion: { required: true },
    },
    messages: {
      Nombre: { required: "Debe escribir un nombre al proveedor" },
      Email: { required: "Debe ingresar un email de contacto" },
      Telefono: { required: "Debe digitar un teléfono de contacto" },
      Direccion: { required: "Debe ingresar la dirección del proveedor" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      $.ajax({
        url: "/Proveedores/Guardar",
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
