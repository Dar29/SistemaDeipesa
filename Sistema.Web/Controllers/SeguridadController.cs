using Sistema.Modelo.Servicios;
using Sistema.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly SeguridadServicio _seguridadServicio;
        public SeguridadController() 
        {
            _seguridadServicio = new SeguridadServicio();
        }
        // GET: Seguridad
        public ActionResult IniciarSesion()
        {
            _seguridadServicio.CerrarSesion();
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(LoginViewModel model)
        {
            var resultado = _seguridadServicio.IniciarSesion(model.Usuario, model.Contraseña);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CerrarSesion()
        {
            var resultado = _seguridadServicio.CerrarSesion();

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}