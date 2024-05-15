"use strict";

var datatable;

$(() => {
  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Home/ListaMateriales",
      type: "GET",
      dataType: "json",
    },
    columns: [
      { data: "IdMaterial" },
      { data: "Nombre" },
      { data: "Descripcion" },
      { data: "Categoria" },
      { data: "Estado" },
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

  obtenerCategorias();

  validarFormulario();
});

async function obtenerCategorias() {
  const categorias = await $.get("/Materiales/ObtenerCatalogoCategorias");
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

  if (data) {
    $("#txtidmaterial").val(data.IdMaterial);
    $("#txtnombre").val(data.Nombre);
    $("#txtdescripcion").val(data.Descripcion);
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
      // IdCategoria: { required: true },
      Nombre: { required: true },
      Descripcion: { required: true },
    },
    messages: {
      IdEstadoMaterial: { required: "Debe seleccionar un estado del material" },
      // IdCategoria: { required: "Debe seleccionar una categoria del material" },
      Nombre: { required: "Debe proporcionar un nombre del material" },
      Descripcion: {
        required: "Debe proporcionar una descripcion del material",
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
