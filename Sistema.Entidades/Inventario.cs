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
        public Material IdMaterial { get; set; }
        public Usuarios IdUsuario { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
