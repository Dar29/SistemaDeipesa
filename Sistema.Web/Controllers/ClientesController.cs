using Sistema.Entidades.Modelos;
using Sistema.Modelo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteServicio _clienteServicio;

        public ClientesController()
            => _clienteServicio = new ClienteServicio();

        public ActionResult Index()
            => View();

        [HttpGet]
        public JsonResult Obtener()
        {
            var servicios = _clienteServicio.Obtener();
            return Json(servicios, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPorId(int id)
        {
            var servicios = _clienteServicio.ObtenerPorId(id);
            return Json(servicios, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Tbl_Cliente modelo)
        {
            var resultado = _clienteServicio.InsertarOActualizar(modelo);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}