using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class DetalleOrdenCompra
    {
        public int IdDetalleOrdenCompra { get; set; }
        public OrdenCompra IdOrdenCompra { get; set; }
        public Material IdMaterial { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }

    }
}
