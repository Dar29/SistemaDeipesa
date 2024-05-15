"use strict";
var datatable;
$(() => {
  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Home/ListaInventario",
      type: "GET",
      dataType: "json",
    },

    columns: [
      { data: "IdInventario" },
      { data: "IdMaterial" },
      { data: "Material" },
      { data: "Cantidad" },
      { data: "PrecioUnitario" },
      { data: "PrecioTotal" },
      { data: "Moneda" },
      { data: "UsuarioIngreso" },
      { data: "Observacion" },
      { data: "FechaIngreso" },
      {
        defaultContent:
          '<button type="button" class="btn btn-primary btn-sm btn-editar"><i class="fas fa-pen"></i></button>' +
          '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar"><i class="fas fa-trash"></i></button>' +
          '<button type="button" class="btn btn-info btn-sm ms-2 btn-detalle"><i class="fas fa-eye"></i></button>',
        orderable: false,
        searchable: false,
        width: "100px",
      },
    ],
    columnDefs: [
      {
        targets: 9,
        render: (data) => moment(data).format("DD/MM/YYYY"),
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

  $("#tabla tbody").on("click", ".btn-eliminar", function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    $.ajax({
      url: `/Inventario/Desactivar`,
      type: "POST",
      data: { id: data.IdInventario },
      success: () => datatable.ajax.reload(),
    });
  });

  $("#tabla tbody").on("click", ".btn-editar", async function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    const inventario = await $.get(
      `/Inventario/ObtenerPorId?id=${data.IdInventario}`
    );

    abrirModal(inventario);
  });

  $("#tabla tbody").on("click", ".btn-detalle", async function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    abrirModalTransacciones(data.IdInventario);
  });

  $("#btnCrearRegistro").on("click", () => abrirModal());

  obtenerMateriales();
  obtenerMonedas();
  validarFormulario();
});

let detalleDatatable;

async function abrirModalTransacciones(id) {
  if (detalleDatatable) detalleDatatable.destroy();
  
  detalleDatatable = $("#tablaTransaccion").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Inventario/ObtenerTransaccionesDeInventario?idInventario=" + id,
      type: "GET",
      dataType: "json",
      dataSrc: "",
    },
    columns: [
      { data: "IdTracking" },
      { data: "Fecha" },
      { data: "Movimiento" },
      { data: "Cantidad" },
      { data: "Observacion" },
      { data: "Usuario" },
      { data: "DescripcionMaterial" },
    ],
    columnDefs: [
      {
        targets: 1,
        render: (data) => moment(data).format("DD/MM/YYYY"),
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

  $("#modalTransaccion").modal("show");
}

async function obtenerMateriales() {
  const materiales = await $.get("/Materiales/ObtenerMateriales");
  const select = $("#selectMaterial");

  select.select2({
    dropdownParent: $("#FormModal"),
    width: "100%",
  });

  materiales.forEach(function (categoria) {
    var option = new Option(categoria.Nombre, categoria.IdMaterial);
    select.append(option);
  });
}

async function obtenerMonedas() {
  const monedas = await $.get("/Monedas/ObtenerMonedas");
  const select = $("#selectMoneda");

  select.select2({
    dropdownParent: $("#FormModal"),
    width: "100%",
  });

  monedas.forEach(function (categoria) {
    var option = new Option(categoria.Descripcion, categoria.IdMoneda);
    select.append(option);
  });
}

function abrirModal(json) {
  $("#txtid").val("");
  $("#selectMaterial").val("").trigger("change");
  $("#selectMoneda").val("").trigger("change");
  $("#txtFechaEntrada").val("");
  $("#txtObservacion").val("");
  $("#txtCantidad").val("");
  $("#txtPrecioUnitario").val("");
  $("#txtPrecioTotal").val("");

  console.log(json);

  if (json != null) {
    const fechaEntrada = moment(json["FechaEntrada"]).format("YYYY-MM-DD");

    $("#txtid").val(json[$("#txtid").prop("name")]);
    $("#selectMaterial")
      .val(json[$("#selectMaterial").prop("name")])
      .trigger("change");
    $("#selectMoneda")
      .val(json[$("#selectMoneda").prop("name")])
      .trigger("change");
    $("#txtFechaEntrada").val(fechaEntrada).trigger("change");
    $("#txtObservacion").val(json[$("#txtObservacion").prop("name")]);
    $("#txtCantidad").val(json[$("#txtCantidad").prop("name")]);
    $("#txtPrecioUnitario").val(json[$("#txtPrecioUnitario").prop("name")]);
    $("#txtPrecioTotal").val(json[$("#txtPrecioTotal").prop("name")]);
  }

  $("#FormModal").modal("show");
}

function validarFormulario() {
  $("form[name='frmInventario']").validate({
    rules: {
      IdMaterial: { required: true },
      IdMoneda: { required: true },
      Cantidad: { required: true },
      PrecioUnitario: { required: true },
      PrecioTotal: { required: true },
      FechaEntrada: { required: true },
    },
    messages: {
      IdMaterial: { required: "Debe seleccionar un material" },
      IdMoneda: { required: "Debe seleccionar una moneda" },
      Cantidad: { required: "Debe ingresar un monto para la cantidad" },
      PrecioUnitario: {
        required: "Debe ingresar un monto para el precio unitario",
      },
      PrecioTotal: { required: "Debe ingresar un monto para precio total" },
      FechaEntrada: { required: "Debe definir la fecha de entrada" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      $.ajax({
        url: "/Inventario/Guardar",
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
