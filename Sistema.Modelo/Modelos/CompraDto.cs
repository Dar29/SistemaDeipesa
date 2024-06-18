using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Modelos
{
    public class CompraDto
    {
        public int IdCompra { get; set; }

        public string NombreProveedor { get; set; }

        public DateTime FechaEmision { get; set; }

        public string Estado { get; set; }

        public decimal? Impuesto { get; set; }

        public decimal? Descuento { get; set; }

        public decimal Total { get; set; }

        public int IdUsuario { get; set; }
    }
}
