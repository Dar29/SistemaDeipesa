const productos = [];
let datatable;
let materiales;

$(() => {
  obtenerProveedores();
  obtenerMateriales();

  validarFormularioMaterial();
  validarFormularioCompra();

  cargarTabla();

  $("#btnAgregar").on("click", () => abrirModal());
});

function cargarTabla() {
  datatable = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    data: productos,
    columns: [
      { data: "IdMaterial" },
      { data: "NombreMaterial" },
      { data: "Cantidad" },
      { data: "Costo" },
      { data: "Subtotal" },
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
}

async function obtenerMateriales() {
  materiales = await $.get("/Materiales/ObtenerMateriales");
  const select = $("#ddlMaterial");

  select.select2({
    dropdownParent: $("#FormModal"),
    width: "100%",
  });

  materiales.forEach(function (categoria) {
    var option = new Option(categoria.Nombre, categoria.IdMaterial);
    select.append(option);
  });
}

function validarFormularioMaterial() {
  $("form[name='frmMaterial']").validate({
    rules: {
      IdMaterial: { required: true },
      Cantidad: { required: true },
      Costo: { required: true },
    },
    messages: {
      IdMaterial: { required: "Debe seleccionar un material" },
      Cantidad: { required: "Debe digitar una cantidad" },
      Costo: { required: "Debe digitar un costo" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      const { Cantidad, Costo, IdMaterial } = Object.fromEntries(
        formData.entries()
      );

      const { Nombre } =
        materiales.find((x) => x.IdMaterial == IdMaterial) ?? {};

      if (!Nombre) return alert("No se encontró el producto");

      productos.push({
        Cantidad: +Cantidad,
        Costo: +Costo,
        NombreMaterial: Nombre,
        IdMaterial: +IdMaterial,
        Subtotal: Math.round(+Cantidad * +Costo),
      });

      datatable.destroy();
      cargarTabla();
      $("#FormModal").modal("hide");
    },
  });
}

function validarFormularioCompra() {
  $("form[name='frmCompra']").validate({
    rules: {
      IdProveedor: { required: true },
      FechaEmision: { required: true },
    },
    messages: {
      IdProveedor: { required: "Debe seleccionar un proveedor" },
      FechaEmision: { required: "Debe proporcionar una fecha" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      if (!productos.length) return alert("Debe ingresar al menos un producto");

      const data = Object.fromEntries(formData.entries());

      $.ajax({
        url: "/Compras/Guardar",
        type: "POST",
        data: JSON.stringify({
          ...data,
          Tbl_DetalleOrdenCompra: productos,
          IdProveedor: +data.IdProveedor,
          Impuesto: +data.Impuesto,
          Descuento: +data.Descuento,
        }),
        processData: false,
        contentType: "application/json",
        success: ({ Exitoso, Mensaje }) => {
          if (!Exitoso) return alert(Mensaje);

          $("#FormModal").modal("hide");
          window.location.href = "/Compras";
        },
      });
    },
  });
}

async function obtenerProveedores() {
  const listado = await $.get("/Proveedores/Obtener");
  const select = $("#ddlProveedor");

  select.select2({
    width: "100%",
  });

  listado.forEach(function (item) {
    var option = new Option(item.Nombre, item.IdProveedor);
    select.append(option);
  });
}

function abrirModal(json) {
  $("#txtCosto").val("");
  $("#txtCantidad").val("");
  $("#ddlMaterial").val("").trigger("change");

  if (json != null) {
    $("#txtCosto").val(json.Costo);
    $("#txtCantidad").val(json.Cantidad);
    $("#ddlMaterial").val(json.IdMaterial).trigger("change");
  }

  $("#FormModal").modal("show");
}
