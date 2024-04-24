using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public Rol IdRol { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set;}
        public string Alias { get; set; }
        public string Identificacion { get; set;}
        public string Email { get; set;}
        public string Telefono { get; set;}
        public string Direccion { get; set;}
        public byte[] PasswordHash { get; set;}
        public byte[] PasswordSalt { get; set;}
        public int Estado { get; set;}
    }
}
