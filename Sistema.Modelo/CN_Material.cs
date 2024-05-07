using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Modelo
{
    public class CN_Material
    {
        private CD_Material objSistemaDatos = new CD_Material();

        public List<VMaterialesDetalle> Listar()
        {
            return objSistemaDatos.Listar();
        }

    }
}
