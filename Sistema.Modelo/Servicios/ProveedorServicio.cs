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
    public class ProveedorServicio : ServicioBase
    {
        public ProveedorServicio() : base() { }

        public IEnumerable<Tbl_Proveedor> Obtener()
        {
            _contexto.Configuration.ProxyCreationEnabled = false;
            var proveedores = _contexto.Tbl_Proveedor.ToList();

            return proveedores;
        }

        public Tbl_Proveedor ObtenerPorId(int id)
        {
            _contexto.Configuration.ProxyCreationEnabled = false;
            var proveedor = _contexto.Tbl_Proveedor.FirstOrDefault(x => x.IdProveedor == id);

            return proveedor;
        }

        public Resultado InsertarOActualizar(Tbl_Proveedor modelo)
        {
            try
            {
                if (modelo.IdProveedor == default)
                {
                    _contexto.Entry(modelo).State = EntityState.Added;
                }
                else
                {
                    var entidad = _contexto.Tbl_Proveedor.FirstOrDefault(x => x.IdProveedor == modelo.IdProveedor);

                    if (entidad is null)
                        return new Resultado(false, "No se ha encontrado el proveedor que desea actualizar");

                    entidad.Estado = modelo.Estado;
                    entidad.Nombre = modelo.Nombre;
                    entidad.Direccion = modelo.Direccion;
                    entidad.Telefono = modelo.Telefono;
                    entidad.Email = modelo.Email;
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
