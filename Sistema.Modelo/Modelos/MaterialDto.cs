using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Modelos
{
    public class MaterialDto
    {
        public int IdMaterial { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string DescripcionCategoria { get; set; }

        public string Estado { get; set; }

        public int Cantidad { get; set; }

        public string DescripcionMoneda { get; set; }
        
        public decimal PrecioUnitario { get; set; }
    }   
}
