using Sistema.Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Modelos
{
    public class InventarioTrackingDto
    {
        public int IdTracking { get; set; }

        public string DescripcionMovimiento { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

        public string Observacion { get; set; }

        public string NombreUsuario { get; set; }
    }
}

