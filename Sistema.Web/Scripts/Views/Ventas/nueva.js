let productos = [];
let datatable;
let materiales;

const controllerName = window.location.pathname.split("/")[1];

$(() => {
  obtenerClientes();
  obtenerMateriales();
  obtenerTipoPagos();

  $("#txtFecha").val(moment().format("yyyy-MM-DD"));

  validarFormularioMaterial();
  validarFormularioVenta();

  $("#txtSubtotal").val(0);
  $("#txtImpuesto").val(0);
  $("#txtTotal").val(0);
  $("#txtDescuento").val(0);
  cargarTabla();

  $("#txtDescuento").on("change", calcularTotales);

  $("#btnAgregar").on("click", () => abrirModal());

  $("#tabla tbody").on("click", ".btn-eliminar", function () {
    var filaSeleccionada = $(this).closest("tr");

    var data = datatable.row(filaSeleccionada).data();

    productos = productos.filter((x) => x.IdMaterial !== data.IdMaterial);
    datatable.destroy();
    cargarTabla();
    calcularTotales();
  });
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
      { data: "PrecioUnitario" },
      { data: "Subtotal" },
      {
        defaultContent:
          '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar"><i class="fas fa-trash"></i></button>',
        orderable: false,
        searchable: false,
        width: "35px",
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
    },
    messages: {
      IdMaterial: { required: "Debe seleccionar un material" },
      Cantidad: { required: "Debe digitar una cantidad" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      const { Cantidad, IdMaterial } = Object.fromEntries(formData.entries());

      const { Nombre, PrecioUnitario } =
        materiales.find((x) => x.IdMaterial == IdMaterial) ?? {};

      if (!Nombre) return alert("No se encontró el producto");

      productos.push({
        Cantidad: +Cantidad,
        PrecioUnitario,
        NombreMaterial: Nombre,
        IdMaterial: +IdMaterial,
        Subtotal: Math.round(+Cantidad * PrecioUnitario),
      });

      calcularTotales();

      datatable.destroy();
      cargarTabla();
      $("#FormModal").modal("hide");
    },
  });
}

function calcularTotales() {
  const subtotal = productos.reduce(
    (accum, producto) => accum + producto.Cantidad * producto.PrecioUnitario,
    0
  );

  if (!subtotal) {
    $("#txtSubtotal").val(0);
    $("#txtImpuesto").val(0);
    $("#txtTotal").val(0);
    $("#txtDescuento").val(0);
    return;
  }

  const descuento = isNaN(+$("#txtDescuento").val())
    ? 0
    : +$("#txtDescuento").val();

  const iva = (subtotal - descuento) * 0.15;

  $("#txtSubtotal").val(subtotal);
  $("#txtImpuesto").val(iva);
  $("#txtTotal").val(subtotal - descuento + iva);
}

function validarFormularioVenta() {
  $("form[name='frmCompra']").validate({
    rules: {
      IdCliente: { required: true },
      Fecha: { required: true },
    },
    messages: {
      IdCliente: { required: "Debe seleccionar un proveedor" },
      Fecha: { required: "Debe proporcionar una fecha" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      if (!productos.length) return alert("Debe ingresar al menos un producto");

      const data = Object.fromEntries(formData.entries());

      $.ajax({
        url: `/${controllerName}/Guardar`,
        type: "POST",
        data: JSON.stringify({
          ...data,
          Tbl_DetalleFactura: productos,
          IdCliente: +data.IdCliente,
          Impuesto: +data.Impuesto,
          Descuento: +data.Descuento,
          Total: +data.Total,
          Subtotal: +data.Subtotal,
        }),
        processData: false,
        contentType: "application/json",
        success: ({ Exitoso, Mensaje }) => {
          if (!Exitoso) return alert(Mensaje);

          $("#FormModal").modal("hide");
          window.location.href = `/${controllerName}`;
        },
      });
    },
  });
}

async function obtenerClientes() {
  const listado = await $.get("/Clientes/Obtener");
  const select = $("#ddlCliente");

  select.select2({
    width: "100%",
  });

  listado.forEach(function (item) {
    var option = new Option(
      `${item.Nombres} ${item.Apellidos}`,
      item.IdCliente
    );
    select.append(option);
  });
}

async function obtenerTipoPagos() {
    const listado = await $.get(`/${controllerName}/ObtenerTipoPagos`);
    const select = $("#ddlTipoPagos");
  
    select.select2({
      width: "100%",
    });
  
    listado.forEach(function (item) {
      var option = new Option(
        `${item.Descripcion}`,
        item.IdTipoPagos
      );
      select.append(option);
    });
  }

function abrirModal(json) {
  $("#txtCantidad").val("");
  $("#ddlMaterial").val("").trigger("change");

  if (json != null) {
    $("#txtCantidad").val(json.Cantidad);
    $("#ddlMaterial").val(json.IdMaterial).trigger("change");
  }

  $("#FormModal").modal("show");
}
