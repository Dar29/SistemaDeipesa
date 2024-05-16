using Sistema.Datos;
using Sistema.Entidades.Utils;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo
{
    public class CN_Monedas
    {
        private readonly CD_Monedas _objSistemaDatos = new CD_Monedas();

        public List<Moneda> Listar()
            => _objSistemaDatos.Listar();
    }
}
