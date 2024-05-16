using Sistema.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class MonedasController : Controller
    {
        private readonly CN_Monedas _cnMonedas;

        public MonedasController()
        {
            _cnMonedas = new CN_Monedas();
        }

        [HttpGet]
        public JsonResult ObtenerMonedas()
        {
            var categorias = _cnMonedas.Listar();
            return Json(categorias, JsonRequestBehavior.AllowGet);
        }
    }
}