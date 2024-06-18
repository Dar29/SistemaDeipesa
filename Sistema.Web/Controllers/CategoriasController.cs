using Sistema.Entidades;
using Sistema.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CN_Categoria cnCategoria;

        public CategoriasController()
        {
            cnCategoria = new CN_Categoria();
        }

        public ActionResult Index() => View();

        [HttpGet]
        public JsonResult ObtenerCatalogoCategorias()
        {
            var categorias = cnCategoria.Listar();
            return Json(categorias, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Guardar(CategoriaMaterial material)
        {
            var resultado = cnCategoria.GuardarOActualizar(material);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPorId(int id)
        {
            var resultado = cnCategoria.ObtenerPorId(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Desactivar(int id)
        {
            var resultado = cnCategoria.Desactivar(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}