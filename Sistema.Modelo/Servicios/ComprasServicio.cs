using Sistema.Entidades;
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
    public class ComprasServicio : ServicioBase
    {
        public ComprasServicio() : base() { }

        public List<CompraDto> ObtenerTodos()
        {
            var listado = (from c in _contexto.Tbl_OrdenCompra
                            select new CompraDto
                            {
                                IdCompra = c.IdOrdenCompra,
                                Descuento = c.Descuento,
                                FechaEmision = c.FechaEmision,
                                Estado = c.Tbl_EstadoOrdenCompra.Descripcion,
                                IdUsuario = c.IdUsuario,
                                Impuesto = c.Impuesto,
                                Total = c.Total,
                                NombreProveedor = c.Tbl_Proveedor.Nombre
                            }).ToList();

            return listado;   
        }

        public Resultado Guardar(Tbl_OrdenCompra compra)
        {
            try
            {
                compra.IdEstadoOrden = 1;
                compra.IdUsuario = _usuario.IdUsuario;
                var subtotal = compra.Tbl_DetalleOrdenCompra.Sum(x => x.Subtotal);
                compra.Total = subtotal - compra.Descuento ?? 0 + compra.Impuesto ?? 0;
                _contexto.Tbl_OrdenCompra.Add(compra);

                var idMateriales = compra.Tbl_DetalleOrdenCompra.Select(x => x.IdMaterial);

                var materiales = _contexto.Tbl_Material.Where(x => idMateriales.Any(y => y == x.IdMaterial));

                foreach (var material in materiales)
                {
                    var detalle = compra.Tbl_DetalleOrdenCompra.FirstOrDefault(x => x.IdMaterial == material.IdMaterial);

                    material.Cantidad += detalle.Cantidad;

                    _contexto.Tbl_InventarioTracking.Add(new Tbl_InventarioTracking
                    {
                        IdMaterial = material.IdMaterial,
                        Cantidad = detalle.Cantidad,
                        Fecha = DateTime.Now,
                        IdUsuario = compra.IdUsuario,
                        IdMov = 1,
                        Observacion = $"Se registra entrada por compra",
                    });

                    _contexto.Entry(material).State = EntityState.Modified;
                }

                _contexto.SaveChanges();
                    
                return new Resultado(true, "Se ha guardado exitosamente la compra");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }

        public Resultado AnularCompra(int id)
        {
            try
            {
                var compra = _contexto.Tbl_OrdenCompra.FirstOrDefault(x => x.IdOrdenCompra == id);

                if (compra is null)
                    return new Resultado(false, "No se ha encontrado la compra que desea anular");

                if (compra.IdEstadoOrden == 2)
                    return new Resultado(false, "La compra ya ha sido anulada");

                compra.IdEstadoOrden = 2;

                var idMateriales = compra.Tbl_DetalleOrdenCompra.Select(x => x.IdMaterial);

                foreach (var detalle in compra.Tbl_DetalleOrdenCompra)
                {

                    detalle.Tbl_Material.Cantidad -= detalle.Cantidad;

                    if (detalle.Tbl_Material.Cantidad < 0)
                        return new Resultado(false, $"El producto {detalle.Tbl_Material.Descripcion} ya fue extraído del inventario, por lo que no puede anular la compra");

                    detalle.Tbl_Material.Tbl_InventarioTracking.Add(new Tbl_InventarioTracking
                    {
                        Cantidad = detalle.Cantidad,
                        Fecha = DateTime.Now,
                        IdUsuario = compra.IdUsuario,
                        IdMov = 8,
                        Observacion = $"Se anula la compra #{compra.IdOrdenCompra}",
                    });

                    _contexto.Entry(detalle.Tbl_Material).State = EntityState.Modified;
                }

                _contexto.Entry(compra).State = EntityState.Modified;
                _contexto.SaveChanges();

                return new Resultado(true, "Se ha anulado exitosamente la compra");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }
    }
}
