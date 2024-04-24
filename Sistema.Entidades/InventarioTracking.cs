using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class InventarioTracking
    {
        public int IdTracking { get; set; }
        public Inventario IdInventario { get; set; }
        public TipoMovimientoInventario IdMov { get; private set; }
        public DateTime Fecha { get; set; }
    }
}
