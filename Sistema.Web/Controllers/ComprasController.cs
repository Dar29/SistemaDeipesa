using Sistema.Entidades.Modelos;
using Sistema.Modelo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ComprasServicio _servicio;
        public ComprasController() 
        {
            _servicio = new ComprasServicio();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nueva()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerTodos()
        {
            var listado = _servicio.ObtenerTodos();

            return Json(listado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Guardar(Tbl_OrdenCompra compra)
        {
            var listado = _servicio.Guardar(compra);

            return Json(listado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Anular(int id)
        {
            var listado = _servicio.AnularCompra(id);

            return Json(listado, JsonRequestBehavior.AllowGet);
        }
    }
}