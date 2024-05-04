using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema.Entidades;
using Sistema.Modelo;

namespace Sistema.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Materiales()
        {
            return View();
        }

        public ActionResult Reportes()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Categorias()
        {
            return View();
        }

        public ActionResult Inventario()
        {
            return View();
        }

        public ActionResult Compras()
        {
            return View();
        }
        public ActionResult Ventas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaUsuarios()
        {
            List<Usuarios> oLista = new List<Usuarios>();

            oLista = new CN_Usuario().Listar();

            return Json(new { data = oLista },JsonRequestBehavior.AllowGet);

        }

    }
}