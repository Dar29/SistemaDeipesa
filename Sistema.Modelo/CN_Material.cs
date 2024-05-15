using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;
using Sistema.Entidades.Utils;

namespace Sistema.Modelo
{
    public class CN_Material
    {
        private CD_Material objSistemaDatos = new CD_Material();

        public List<VMaterialesDetalle> Listar()
        {
            return objSistemaDatos.Listar();
        }

        public Resultado GuardarOActualizar(Material obj)
            => objSistemaDatos.GuardarOActualizar(obj);

        public Material ObtenerPorId(int id)
            => objSistemaDatos.ObtenerPorId(id);

        public Resultado Desactivar(int id)
            => objSistemaDatos.Desactivar(id);
    }
}
