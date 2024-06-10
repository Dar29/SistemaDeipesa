$(() => {
  obtenerMateriales();
  obtenerCategorias();
  obtenerUsuarios();

  $("#btnExportar").on("click", Exportar);

  validarFormulario();
});

function validarFormulario() {
  $("form[name='frmReporte']").validate({
    rules: {},
    messages: {},
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);
      Visualizar();

      cargarTabla(
        formData.get("Fecha"),
        formData.get("IdUsuario"),
        formData.get("IdMaterial"),
        formData.get("IdCategoria")
      );
    },
  });
}

async function obtenerUsuarios() {
  const materiales = await $.get("/Home/ListaUsuarios");
  const select = $("#ddlUsuario");

  select.select2({
    width: "100%",
  });

  materiales.data.forEach(function (categoria) {
    var option = new Option(
      categoria.Nombres + " " + categoria.Apellidos,
      categoria.IdUsuario
    );
    select.append(option);
  });
}

async function obtenerMateriales() {
  const materiales = await $.get("/Materiales/ObtenerMateriales");
  const select = $("#ddlMaterial");

  select.select2({
    width: "100%",
  });

  materiales.forEach(function (categoria) {
    var option = new Option(categoria.Nombre, categoria.IdMaterial);
    select.append(option);
  });
}

async function obtenerCategorias() {
  const categorias = await $.get("/Categorias/ObtenerCatalogoCategorias");
  const select = $("#ddlCategoria");

  select.select2({
    width: "100%",
  });

  categorias.forEach(function (categoria) {
    var option = new Option(categoria.Descripcion, categoria.IdCategoria);
    select.append(option);
  });
}

var tabladata;
function cargarTabla(fecha, idUsuario, idMaterial, idCategoria) {
  tabladata?.destroy();
  tabladata = $("#tabla").DataTable({
    responsive: true,
    ordering: true,
    ajax: {
      url: "/Home/ListaInventario",
      type: "GET",
      dataType: "json",
      data: {
        fecha,
        idUsuario,
        idMaterial,
        idCategoria,
      },
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
    columnDefs: [
      {
        targets: 9,
        render: (data) => moment(data).format("DD/MM/YYYY"),
      },
    ],
  });
}

function Visualizar() {
  document.getElementById("tabladiv").classList.remove("d-none");
  document.getElementById("tabla").classList.remove("d-none");
}

function Exportar() {
  // Obtener la tabla y sus filas
  const table = document.getElementById("tabla");
  const rows = table.querySelectorAll("tr");

  // Crear un nuevo libro de Excel
  const workbook = XLSX.utils.book_new();

  // Crear una hoja de cálculo
  const sheet = XLSX.utils.table_to_sheet(table);

  // Agregar encabezado "REPORTE DE INVENTARIO"
  XLSX.utils.sheet_add_aoa(sheet, [["REPORTE DE INVENTARIO"]], {
    origin: [0, 0],
    raw: true,
  });

  // Agregar fila para fecha de generación
  const fechaGeneracion = new Date().toLocaleDateString();
  XLSX.utils.sheet_add_aoa(sheet, [["Fecha de generación:", fechaGeneracion]], {
    origin: [1, 0],
    raw: true,
  });

  // Agregar nombres de columnas en negrita
  const headerRow = sheet["2"]; // Cambiado el índice para que sea la tercera fila (0-indexed)
  for (let cell in headerRow) {
    if (headerRow.hasOwnProperty(cell)) {
      headerRow[cell].s = { font: { bold: true } };
    }
  }

  // Agregar la hoja de cálculo al libro
  XLSX.utils.book_append_sheet(workbook, sheet, "Inventario");

  // Guardar el libro como archivo Excel
  XLSX.writeFile(workbook, "Reporte Inventario.xlsx");
}
