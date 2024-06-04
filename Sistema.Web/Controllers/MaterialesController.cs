using Sistema.Entidades;
using Sistema.Entidades.Modelos;
using Sistema.Modelo;
using Sistema.Modelo.Servicios;
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

        private readonly MaterialServicio _materialServicio;

        public MaterialesController() 
        {
            cnCategoria = new CN_Categoria();
            cnMaterial = new CN_Material();
            _materialServicio = new MaterialServicio();
        }

        [HttpGet]
        public JsonResult ObtenerMateriales()
        {
            var categorias = cnMaterial.Listar();
            return Json(categorias, JsonRequestBehavior.AllowGet);
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