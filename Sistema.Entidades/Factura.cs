using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public Cliente IdCliente { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set;}
        public decimal Total { get; set;}
        public TipoPagos IdTipoPagos { get; set; }
        public DateTime Fecha { get; set;}
    }
}
