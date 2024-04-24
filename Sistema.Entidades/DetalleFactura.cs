using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public Factura IdFactura { get; set; }
        public Material IdMaterial { get; set;}
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime Fecha { get; set; }
    }
}
