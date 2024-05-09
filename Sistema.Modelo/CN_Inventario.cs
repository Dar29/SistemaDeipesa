using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Modelo
{
    public class CN_Inventario
    {
        private CD_Inventario objSistemaDatos = new CD_Inventario();

        public List<VInventario> Listar()
        {
            return objSistemaDatos.Listar();
        }

    }
}
