using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;
using Sistema.Entidades.Utils;
using Sistema.Modelo.Servicios;

namespace Sistema.Modelo
{
    public class CN_Inventario : ServicioBase
    {
        private CD_Inventario objSistemaDatos = new CD_Inventario();
        private CD_Usuario objUsuarios = new CD_Usuario();

        public CN_Inventario() : base() { }

        public IEnumerable<VInventario> Listar(DateTime? fecha, int? idMaterial, int? idCategoria, int? idUsuario)
        {
            var query = _contexto.Tbl_InventarioTracking.Where(x => true);

            if (fecha != null)
                query = query.Where(x => DbFunctions.TruncateTime(x.Fecha) == fecha.Value);

            if (idMaterial != null && idMaterial != default)
                query = query.Where(x => x.IdMaterial == idMaterial);

            if (idCategoria != null && idCategoria != default)
                query = query.Where(x => x.Tbl_Material.IdCategoria == idCategoria);

            if (idUsuario != null && idUsuario != default)
                query = query.Where(x => x.IdUsuario == idUsuario);

            var reporte = query.Select(x => new VInventario
            {
                Cantidad = x.Cantidad,
                FechaIngreso = x.Fecha,
                Observacion = x.Observacion,
                IdMaterial = x.IdMaterial,
                Material = x.Tbl_Material.Nombre,
                UsuarioIngreso = x.Tbl_Usuario.Usuario,
                IdInventario = x.IdTracking,
                PrecioTotal = (x.Tbl_Material.PrecioUnitario * x.Cantidad),
                PrecioUnitario = x.Tbl_Material.PrecioUnitario,
                Moneda = x.Tbl_Material.Tbl_Moneda.Descripcion
            }).ToList();

            return reporte;
        }

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
