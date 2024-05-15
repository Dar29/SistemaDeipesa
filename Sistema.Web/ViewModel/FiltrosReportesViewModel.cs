using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Web.ViewModel
{
    public class FiltrosReportesViewModel
    {
        [Display(Name = "Fecha Ingreso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaIngreso { get; set; }

        [Display(Name = "Material")]
        public int? Material {  get; set; }

        public IEnumerable<SelectListItem> ListaMateriales { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
        public IEnumerable<SelectListItem> ListaUsuarios { get; set; }


    }
}