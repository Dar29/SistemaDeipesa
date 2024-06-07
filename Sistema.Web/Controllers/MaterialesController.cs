using Sistema.Entidades;
using Sistema.Entidades.Modelos;
using Sistema.Modelo;
using Sistema.Modelo.Servicios;
using Sistema.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly CN_Material cnMaterial;

        private readonly MaterialServicio _materialServicio;

        public MaterialesController() 
        {
            cnMaterial = new CN_Material();
            _materialServicio = new MaterialServicio();
        }

        public ActionResult Index() => View();

        [HttpGet]
        public JsonResult ObtenerMateriales()
        {
            var resultado = _materialServicio.ObtenerTodos();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Guardar(Tbl_Material material)
        {
            var resultado = _materialServicio.InsertarOActualizar(material);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPorId(int id)
        {
            var resultado = _materialServicio.ObtenerPorId(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Desactivar(int id)
        {
            var resultado = cnMaterial.Desactivar(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerMovimientosPorIdMaterial(int idMaterial)
        {
            var resultado = _materialServicio.ObtenerMovimientosPorIdMaterial(idMaterial);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerarSalidaPorDaño(SalidaPorDañoViewModel modelo)
        {
            var resultado = _materialServicio.GenerarSalidaPorDaño(modelo.IdMaterial, modelo.Cantidad, modelo.Observacion);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}