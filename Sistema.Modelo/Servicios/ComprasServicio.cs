using Sistema.Entidades.Modelos;
using Sistema.Entidades.Utils;
using Sistema.Modelo.Modelos;
using System;
using System.Collections.Generic;
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
                compra.IdUsuario = 1;
                var subtotal = compra.Tbl_DetalleOrdenCompra.Sum(x => x.Subtotal);
                compra.Total = subtotal - compra.Descuento ?? 0 + compra.Impuesto ?? 0;

                _contexto.Tbl_OrdenCompra.Add(compra);
                _contexto.SaveChanges();
                return new Resultado(true, "Se ha guardado exitosamente la compra");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }
    }
}
