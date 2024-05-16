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
    public class CN_Inventario
    {
        private CD_Inventario objSistemaDatos = new CD_Inventario();
        private CD_Usuario objUsuarios = new CD_Usuario();

        public List<VInventario> Listar()
            => objSistemaDatos.Listar();

        public List<VInventarioTracking> ListarTransaccionesDeInventario(int idInventario)
            => objSistemaDatos.ListarTransaccionesDeInventario(idInventario);


        public Resultado GuardarOActualizar(Inventario obj)
        {
            // TODO: Reemplazar con el usuario que realiza la consulta, para ello se debe completar el login y autenticacion en el sistema
            var idUsuario = objUsuarios.Listar().FirstOrDefault().IdUsuario;

            obj.IdUsuario = idUsuario;
            return objSistemaDatos.GuardarOActualizar(obj);
        }

        public Inventario ObtenerPorId(int id)
            => objSistemaDatos.ObtenerPorId(id);

        public Resultado Desactivar(int id)
            => objSistemaDatos.Desactivar(id);
    }
}
