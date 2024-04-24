using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Material
    {
        public int IdMaterial { get; set; }
        public EstadoMaterial IdEstadoMaterial { get; set; }
        public CategoriaMaterial IdCategoria { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Descripcion { get; set; }
    }
}
