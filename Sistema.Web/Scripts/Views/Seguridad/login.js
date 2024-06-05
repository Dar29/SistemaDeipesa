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
