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
    public class CN_Categoria
    {
        private CD_Categoria objSistemaDatos = new CD_Categoria();

        public List<CategoriaMaterial> Listar()
            => objSistemaDatos.Listar();

        public Resultado GuardarOActualizar(CategoriaMaterial obj)
            => objSistemaDatos.GuardarOActualizar(obj);

        public CategoriaMaterial ObtenerPorId(int id)
            => objSistemaDatos.ObtenerPorId(id);

        public Resultado Desactivar(int id)
            => objSistemaDatos.Desactivar(id);
    }
}
