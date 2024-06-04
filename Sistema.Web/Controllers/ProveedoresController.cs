using Sistema.Entidades;
using Sistema.Entidades.Modelos;
using Sistema.Modelo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ProveedorServicio _proveedorServicio;

        public ProveedoresController()
        {
            _proveedorServicio = new ProveedorServicio();
        }

        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Obtener()
        {
            var servicios = _proveedorServicio.Obtener();
            return Json(servicios, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPorId(int id)
        {
            var servicios = _proveedorServicio.ObtenerPorId(id);
            return Json(servicios, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Tbl_Proveedor modelo)
        {
            var resultado = _proveedorServicio.InsertarOActualizar(modelo);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}