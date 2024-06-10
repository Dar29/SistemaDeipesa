using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Modelos
{
    public class DetalleFacturaDto
    {
        public int Cantidad { get; set; }
        public string NombreMaterial { get; set; }
        public decimal Subtotal { get; set; }
    }
}
