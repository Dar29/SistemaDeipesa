"use strict";

$(() => {
  window.history.forward(0);
  $("form[name='frmLogin']").validate({
    rules: {
      Usuario: { required: true },
      Contraseña: { required: true },
    },
    messages: {
      Usuario: { required: "Debe ingresar un usuario" },
      Contraseña: { required: "Debe ingresar una contraseña" },
    },
    submitHandler: (form) => {
      if (!$(form).valid()) return;

      var formData = new FormData(form);

      $.ajax({
        url: "/Seguridad/IniciarSesion",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: ({ Exitoso, Mensaje }) => {
          if (!Exitoso) return alert(Mensaje);

          window.location.href = "/";
        },
      });
    },
  });
});

document.getElementById('frmLogin').addEventListener('submit', function (event) {
    event.preventDefault(); // Evitar el envío del formulario por defecto

    // Mostrar el mensaje de carga
    document.getElementById('frmLogin').style.display = 'none';
    document.getElementById('loadingMessage').style.display = 'block';

    setTimeout(function () {
        document.getElementById('frmLogin').style.display = 'block';
        document.getElementById('loadingMessage').style.display = 'none';
    }, 3000);
});
