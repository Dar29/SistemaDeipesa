using Sistema.Entidades.Modelos;
using Sistema.Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Servicios
{
    public class VentasServicio : ServicioBase
    {
        private readonly IQueryable<Tbl_Factura> _listadoFacturas;

        public VentasServicio() : base()
            => _listadoFacturas = _contexto.Tbl_Factura.Where(x => x.TipoFactura == "F");        

        public IEnumerable<VentaDto> ObtenerFacturas()
        {
            var facturas = _listadoFacturas.Select(x => new VentaDto
            {
                IdFactura = x.IdFactura,
                TipoFactura = x.TipoFactura,
                Descuento = x.Descuento,
                Fecha = x.Fecha,
                FechaEmision = x.FechaEmision,
                Impuesto = x.Impuesto,
                NombreCliente = x.Tbl_Cliente.Nombres,
                TipoPago = x.Tbl_TipoPagos.Descripcion,
                Total = x.Total
            });

            return facturas;
        }

        
    }
}
