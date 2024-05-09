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

        [HttpGet]
        public JsonResult ListaMateriales()
        {
            List<VMaterialesDetalle> oLista = new List<VMaterialesDetalle>();

            oLista = new CN_Material().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListaInventario()
        {
            List<CategoriaMaterial> oLista = new List<VIn>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }



    }
}