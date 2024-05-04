using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Modelo
{
    public class CN_Usuario
    {
        private CD_Usuario objSistemaDatos = new CD_Usuario();

        public List<Usuarios> Listar()
        {
            return objSistemaDatos.Listar();
        }

    }
}
