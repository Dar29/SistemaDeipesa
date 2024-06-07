using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Modelos
{
    public class VentaDto
    {
        public int IdFactura { get; set; }

        public string NombreCliente { get; set; }

        public DateTime FechaEmision { get; set; }

        public decimal? Impuesto { get; set; }

        public decimal? Descuento { get; set; }

        public decimal Total { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoPago { get; set; }

        public string TipoFactura { get; set; }
    }
}
