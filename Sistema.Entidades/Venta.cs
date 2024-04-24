using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Cliente IdCliente { get; set; }
        public Usuario IdUsuario { get; set; }
        public Factura IdFactura { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalPagado { get; set; }
    }
}
