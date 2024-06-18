using Sistema.Entidades.Modelos;
using Sistema.Entidades.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Servicios
{
    public class ClienteServicio : ServicioBase
    {
        public ClienteServicio() : base() { }

        public IEnumerable<Tbl_Cliente> Obtener()
        {
            _contexto.Configuration.ProxyCreationEnabled = false;
            var clientes = _contexto.Tbl_Cliente.ToList();

            return clientes;
        }

        public Tbl_Cliente ObtenerPorId(int id)
        {
            _contexto.Configuration.ProxyCreationEnabled = false;
            var proveedor = _contexto.Tbl_Cliente.FirstOrDefault(x => x.IdCliente == id);

            return proveedor;
        }

        public Resultado InsertarOActualizar(Tbl_Cliente modelo)
        {
            try
            {
                if (modelo.IdCliente == default)
                {
                    modelo.UsuarioGrabacion = _usuario.Usuario;
                    modelo.FechaGrabacion = DateTime.Now;
                    _contexto.Entry(modelo).State = EntityState.Added;
                }
                else
                {
                    var entidad = _contexto.Tbl_Cliente.FirstOrDefault(x => x.IdCliente == modelo.IdCliente);

                    if (entidad is null)
                        return new Resultado(false, "No se ha encontrado el cliente que desea actualizar");

                    entidad.Identificacion = modelo.Identificacion;
                    entidad.Apellidos = modelo.Apellidos;
                    entidad.Nombres = modelo.Nombres;
                    entidad.Direccion = modelo.Direccion;
                    entidad.Telefono = modelo.Telefono;
                    entidad.Email = modelo.Email;
                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioModificacion = _usuario.Usuario;

                    _contexto.Entry(entidad).State = EntityState.Modified;
                }
                _contexto.SaveChanges();

                return new Resultado(true, "El registro se ha ingresado correctamente");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }
    }
}
