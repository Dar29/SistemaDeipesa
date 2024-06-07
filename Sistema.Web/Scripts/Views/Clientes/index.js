"use strict";

var datatable;

$(() => {
  $("#txtBuscar").on("change", (e) => {
    const texto = $(e.target).val();
    datatable.search(texto).draw();
  });

  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Clientes/Obtener",
      type: "GET",
      dataType: "json",
      dataSrc: "",
    },
    columns: [
      { data: "IdCliente" },
      { data: "Nombres" },
      { data: "Apellidos" },
      { data: "Email" },
      { data: "Telefono" },
      { data: "Direccion" },
      {
        defaultContent:
          '<button type="button" class="btn btn-primary btn-sm btn-editar"><i class="fas fa-pen"></i></button>',
          // '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar"><i class="fas fa-trash"></i></button>',
        orderable: false,
        searchable: false,
        width: "30px",
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
      url: `/Clientes/Desactivar`,
      type: "POST",
      data: { id: data.IdCliente },
      success: () => datatable.ajax.reload(),
    });
  });

  $("#tabla tbody").on("click", ".btn-editar", async function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    const material = await $.get(
      `/Clientes/ObtenerPorId?id=${data.IdCliente}`
    );

    abrirModal(material);
  });

  validarFormulario();
});

function abrirModal(data) {
  $("#txtCliente").val("");
  $("#txtNombres").val("");
  $("#txtApellidos").val("");
  $("#txtEmail").val("");
  $("#txtTelefono").val("");
  $("#txtDireccion").val("");

  if (data) {
    $("#txtIdCliente").val(data.IdCliente);
    $("#txtNombres").val(data.Nombres);
    $("#txtApellidos").val(data.Apellidos);
    $("#txtEmail").val(data.Email);
    $("#txtTelefono").val(data.Telefono);
    $("#txtDireccion").val(data.Direccion);
  }

  $("#FormModal").modal("show");
}

function validarFormulario() {
  $("form[name='frmCliente']").validate({
    rules: {
      Nombres: { required: true },
      Apellidos: { required: true },
      Telefono: { required: true },
      Direccion: { required: true },
    },
    messages: {
      Nombres: { required: "Debe escribir un nombre al proveedor" },
      Apellidos: { required: "Debe ingresar los apellidos del cliente" },
      Telefono: { required: "Debe digitar un teléfono de contacto" },
      Direccion: { required: "Debe ingresar la dirección del proveedor" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      $.ajax({
        url: "/Clientes/Guardar",
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
