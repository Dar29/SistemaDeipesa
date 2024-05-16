using Sistema.Entidades;
using Sistema.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class InventarioController : Controller
    {
        private readonly CN_Categoria _cnCategoria;

        private readonly CN_Material _cnMaterial;

        private readonly CN_Inventario _cnInventario;

        public InventarioController()
        {
            _cnCategoria = new CN_Categoria();
            _cnMaterial = new CN_Material();
            _cnInventario = new CN_Inventario();
        }

        [HttpGet]
        public JsonResult ObtenerTransaccionesDeInventario(int idInventario)
        {
            var transacciones = _cnInventario.ListarTransaccionesDeInventario(idInventario);
            return Json(transacciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Guardar(Inventario material)
        {
            var resultado = _cnInventario.GuardarOActualizar(material);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPorId(int id)
        {
            var resultado = _cnInventario.ObtenerPorId(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Desactivar(int id)
        {
            var resultado = _cnInventario.Desactivar(id);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}