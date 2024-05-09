using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class VMaterialesDetalle
    {
        public int IdMaterial { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Moneda { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; }
    }
}
