using Sistema.Entidades.Modelos;
using Sistema.Modelo.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.Controllers
{
    public class VentasController : Controller
    {
        private readonly VentasServicio _facturaServicio;
        public VentasController() 
            => _facturaServicio = new VentasServicio("F");

        public ActionResult Index()
            => View();

        public ActionResult Nueva()
            => View();

        public ActionResult ObtenerTodos()
            => Json(_facturaServicio.ObtenerFacturas(), JsonRequestBehavior.AllowGet);
        
        [HttpGet]
        public ActionResult ObtenerTipoPagos()
            => Json(new TipoPagosServicio().ObtenerTodos(), JsonRequestBehavior.AllowGet);

        public ActionResult Guardar(Tbl_Factura modelo)
            => Json(_facturaServicio.Guardar(modelo), JsonRequestBehavior.AllowGet);

        public ActionResult Anular(int id)
            => Json(_facturaServicio.AnularFactura(id), JsonRequestBehavior.AllowGet);
    }
}