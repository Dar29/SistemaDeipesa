using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Inventario
    {
        public int IdInventario { get; set; }
        public int IdMaterial { get; set; }
        public int IdUsuario { get; set; }
        public int IdMoneda { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public string Observacion { get; set; }
    }
}
