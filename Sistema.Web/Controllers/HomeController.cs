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
        public JsonResult ListaReporte()
        {
            List<Reporte> oLista = new List<Reporte>();

            // Crear un objeto filtros con los valores adecuados
            filtros objFiltros = new filtros
            {
                // Asignar valores a las propiedades según tus requerimientos
                IdMaterial = 1, // Por ejemplo
                IdCategoria = 2, // Por ejemplo
                IdUsuario = 3, // Por ejemplo
                Stock = 100, // Por ejemplo
                FechaIngreso = DateTime.Now // Por ejemplo
            };

            // Llamar al método ReporteInventarioGeneral con el objeto filtros
            oLista = new CN_Reporte().ReporteInventarioGeneral(objFiltros, out string mensaje);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if(objeto.IdUsuario == 0)
            {
                resultado = new CN_Usuario().Registrar(objeto, out mensaje);
            }
            else {

                resultado = new CN_Usuario().Editar(objeto, out mensaje);           
            
            }

            return Json(new { resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);

        }

        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Usuario().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

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
            List<VInventario> oLista = new List<VInventario>();

            oLista = new CN_Inventario().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<CategoriaMaterial> oLista = new List<CategoriaMaterial>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }



    }
}