using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class VInventario
    {
        public int IdInventario { get; set; }
        public int IdMaterial { get; set; }
        public string Material { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario {  get; set; }
        public decimal PrecioTotal { get; set; }
        public string Moneda { get; set; }
        public string UsuarioIngreso { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaIngreso { get; set; }
      
    }
}
