﻿"use strict";

var datatable;
var detalleDatatable;

$(() => {
  $("#txtBuscar").on("change", (e) => {
    const texto = $(e.target).val();
    datatable.search(texto).draw();
  });

  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Materiales/ObtenerMateriales",
      type: "GET",
      dataType: "json",
      dataSrc: "",
    },
    columns: [
      { data: "IdMaterial" },
      { data: "Nombre" },
      { data: "Descripcion" },
      { data: "DescripcionCategoria" },
      { data: "PrecioUnitario" },
      { data: "DescripcionMoneda" },
      { data: "Cantidad" },
      {
        data: "Estado",
        render: function (valor) {
          if (valor == "Activo") {
            return '<span class="badge bg-success"><i class="fas fa-check"></i></span>';
          } else {
            return '<span class="badge bg-danger"><i class="fas fa-xmark"></i></span>';
          }
        },
      },
      {
        defaultContent:
          "<div>" +
          '<button type="button" class="btn btn-primary btn-sm btn-editar"><i class="fas fa-pen"></i></button>' +
          '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar"><i class="fas fa-trash"></i></button>' +
          '<button type="button" class="btn btn-secondary btn-sm ms-2 btn-historial"><i class="fas fa-clipboard"></i></button>' +
          '<button type="button" class="btn btn-warning btn-sm ms-2 btn-salida"><i class="fas fa-square-arrow-up-right"></i></button>' +
          "</div>",
        orderable: false,
        searchable: false,
        responsivePriority: -1,
        width: "135px",
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
      data: { id: data.IdMaterial },
      success: () => datatable.ajax.reload(),
    });
  });

  $("#tabla tbody").on("click", ".btn-editar", async function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    const material = await $.get(
      `/Materiales/ObtenerPorId?id=${data.IdMaterial}`
    );

    abrirModal(material);
  });

  $("#tabla tbody").on("click", ".btn-salida", async function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    $("#txtIdMaterial").val(data.IdMaterial);
    $("#txtCantidad").val(1);
    $("#modalSalida").modal("show");

    // abrirModal(material);
  });

  $("#tabla tbody").on("click", ".btn-historial", async function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    abrirModalTransacciones(data.IdMaterial);
  });

  obtenerCategorias();
  obtenerMonedas();
  validarFormulario();
  validarFormularioSalida();
});

async function abrirModalTransacciones(id) {
  if (detalleDatatable) detalleDatatable.destroy();

  detalleDatatable = $("#tablaTransaccion").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Materiales/ObtenerMovimientosPorIdMaterial?idMaterial=" + id,
      type: "GET",
      dataType: "json",
      dataSrc: "",
    },
    columns: [
      { data: "IdTracking" },
      { data: "NombreUsuario" },
      { data: "Fecha" },
      { data: "DescripcionMovimiento" },
      { data: "Cantidad" },
      { data: "Observacion" },
    ],
    columnDefs: [
      {
        targets: 2,
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

async function obtenerMonedas() {
  const monedas = await $.get("/Monedas/ObtenerMonedas");
  const select = $("#ddlMoneda");

  select.select2({
    dropdownParent: $("#FormModal"),
    width: "100%",
  });

  monedas.forEach(function (categoria) {
    var option = new Option(categoria.Descripcion, categoria.IdMoneda);
    select.append(option);
  });
}

async function obtenerCategorias() {
  const categorias = await $.get("/Categorias/ObtenerCatalogoCategorias");
  const select = $("#selectcategoria");

  select.select2({
    dropdownParent: $("#FormModal"),
    width: "100%",
  });

  categorias.forEach(function (categoria) {
    var option = new Option(categoria.Descripcion, categoria.IdCategoria);
    select.append(option);
  });
}

function abrirModal(data) {
  $("#txtidmaterial").val("");
  $("#txtnombre").val("");
  $("#txtdescripcion").val("");
  $("#selectactivo").val("");
  $("#txtPrecioUnitario").val("");
  $("#ddlMoneda").val("").trigger("change");

  if (data) {
    $("#txtidmaterial").val(data.IdMaterial);
    $("#txtnombre").val(data.Nombre);
    $("#txtdescripcion").val(data.Descripcion);
    $("#txtPrecioUnitario").val(data.PrecioUnitario);
    $("#ddlMoneda").val(data.IdMoneda).trigger("change");
    $("#selectcategoria").val(data.IdCategoria).trigger("change");
    $("#selectactivo")
      .val(data.IdEstadoMaterial ? 1 : 0)
      .trigger("change");
  }

  $("#FormModal").modal("show");
}

function validarFormulario() {
  $("form[name='frmMaterial']").validate({
    rules: {
      IdEstadoMaterial: { required: true },
      Nombre: { required: true },
      Descripcion: { required: true },
      IdMoneda: { required: true },
      PrecioUnitario: {
        required: true,
      },
    },
    messages: {
      IdEstadoMaterial: { required: "Debe seleccionar un estado del material" },
      // IdCategoria: { required: "Debe seleccionar una categoria del material" },
      Nombre: { required: "Debe proporcionar un nombre del material" },
      Descripcion: {
        required: "Debe proporcionar una descripcion del material",
      },
      IdMoneda: {
        required: "Debe seleccionar una moneda para la venta",
      },
      PrecioUnitario: {
        required: "Debe digitar el precio unitario del material",
      },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      $.ajax({
        url: "/Materiales/Guardar",
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

function validarFormularioSalida() {
  $("form[name='frmSalidaMaterial']").validate({
    rules: {
      Cantidad: { required: true },
      Observacion: { required: true },
    },
    messages: {
      Cantidad: { required: "Debe ingresar una cantidad para la salida" },
      Observacion: { required: "Debe ingresar una observación para la salida" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      $.ajax({
        url: "/Materiales/GenerarSalidaPorDaño",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: ({ Exitoso, Mensaje }) => {
          console.log({ Exitoso, Mensaje });
          if (!Exitoso) return alert(Mensaje);

          $("#modalSalida").modal("hide");
          datatable.ajax.reload();
        },
      });
    },
  });
}
