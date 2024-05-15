using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class VInventarioTracking
    {
        public int IdTracking { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string Observacion { get; set; }
        public string Usuario { get; set; }
        public string DescripcionMaterial { get; set; }
    }
}
