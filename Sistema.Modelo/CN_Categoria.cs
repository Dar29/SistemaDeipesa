using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Modelo
{
    public class CN_Categoria
    {
        private CD_Categoria objSistemaDatos = new CD_Categoria();

        public List<CategoriaMaterial> Listar()
        {
            return objSistemaDatos.Listar();
        }

    }
}
