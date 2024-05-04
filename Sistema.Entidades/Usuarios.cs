using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public Rol IdRol { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set;}
        public string Usuario { get; set; }
        public string Correo { get; set;}
        public string Contrasenia { get; set;}
        public DateTime FechaRegistro { get; set;}
        public DateTime FechaModificacion { get; set;}
        public int Estado { get; set;}
    }
}
