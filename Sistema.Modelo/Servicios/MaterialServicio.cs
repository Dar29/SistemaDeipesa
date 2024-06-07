using Sistema.Entidades.Modelos;
using Sistema.Entidades.Utils;
using Sistema.Modelo.Modelos;
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

        public IEnumerable<MaterialDto> ObtenerTodos()
        {
            var materiales = _contexto.Tbl_Material.Select(x => new MaterialDto
            {
                IdMaterial = x.IdMaterial,
                Cantidad = x.Cantidad,
                Descripcion = x.Descripcion,
                Estado = x.Tbl_EstadoMaterial.Descripcion,
                DescripcionMoneda = x.Tbl_Moneda.Descripcion,
                Nombre = x.Nombre,
                DescripcionCategoria = x.Tbl_Categoria.Descripcion,
                PrecioUnitario = x.PrecioUnitario
            }).ToList();

            return materiales;
        }

        public Tbl_Material ObtenerPorId(int id)
        {
            _contexto.Configuration.ProxyCreationEnabled = false;
            var material = _contexto.Tbl_Material.FirstOrDefault(x => x.IdMaterial == id);

            return material;
        }
    }
}
