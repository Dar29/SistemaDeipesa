using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Utils
{
    public class Resultado
    {
        public bool Exitoso { get; set; }

        public string Mensaje { get; set; }

        public Resultado(bool exitoso, string mensaje)
        {
            Exitoso = exitoso;
            Mensaje = mensaje;
        }
    }
}
