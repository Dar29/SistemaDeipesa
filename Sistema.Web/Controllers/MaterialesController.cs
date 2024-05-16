using Sistema.Entidades;
using Sistema.Modelo;
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
        private readonly CN_Categoria cnCategoria;

        private readonly CN_Material cnMaterial;

        public MaterialesController() 
        {
            cnCategoria = new CN_Categoria();
            cnMaterial = new CN_Material();
        }

        [HttpGet]
        public JsonResult ObtenerMateriales()
        {
            var categorias = cnMaterial.Listar();
            return Json(categorias, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Guardar(Material material)
        {
            var resultado = cnMaterial.GuardarOActualizar(material);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPorId(int id)
        {
            var resultado = cnMaterial.ObtenerPorId(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Desactivar(int id)
        {
            var resultado = cnMaterial.Desactivar(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}