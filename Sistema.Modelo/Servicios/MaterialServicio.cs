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
    public class MaterialServicio : ServicioBase
    {
        public MaterialServicio() : base() { }

        public Resultado InsertarOActualizar(Tbl_Material modelo)
        {
            try
            {
                if (modelo.IdMaterial == default)
                {
                    modelo.FechaRegistro = DateTime.Now;
                    modelo.FechaModificacion = DateTime.Now;
                    _contexto.Entry(modelo).State = EntityState.Added;
                    
                }
                else
                {
                    var entidad = _contexto.Tbl_Material.FirstOrDefault(x => x.IdMaterial == modelo.IdMaterial);
                    entidad.IdEstadoMaterial = modelo.IdEstadoMaterial;
                    entidad.IdMoneda = modelo.IdMoneda;
                    entidad.PrecioUnitario = modelo.PrecioUnitario;
                    entidad.Descripcion = modelo.Descripcion;
                    entidad.Nombre = modelo.Nombre;
                    entidad.IdCategoria = modelo.IdCategoria;

                    _contexto.Entry(entidad).State = EntityState.Modified;
                }

                _contexto.SaveChanges();

                return new Resultado(true, "Se ha guardado el material exitosamente");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }
    }
}
