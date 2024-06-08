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
    public class VentasServicio : ServicioBase
    {
        private readonly IQueryable<Tbl_Factura> _listadoFacturas;

        private readonly string _tipoFactura;

        public VentasServicio(string tipoFactura) : base()
        {
            _listadoFacturas = _contexto.Tbl_Factura.Where(x => x.TipoFactura.Equals(tipoFactura));
            _tipoFactura = tipoFactura;
        }

        public IEnumerable<VentaDto> ObtenerFacturas()
        {
            _contexto.Configuration.ProxyCreationEnabled = false;
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
                Total = x.Total,
                Activo = x.Activo
            }).ToList();

            return facturas;
        }

        public Resultado Guardar(Tbl_Factura factura)
        {
            try
            {
                factura.IdUsuario = _usuario.IdUsuario;
                var subtotal = factura.Tbl_DetalleFactura.Sum(x => x.Subtotal);
                factura.FechaEmision = DateTime.Now;
                factura.TipoFactura = _tipoFactura;
                factura.Total = subtotal - factura.Descuento ?? 0 + factura.Impuesto ?? 0;
                factura.Activo = true;
                _contexto.Tbl_Factura.Add(factura);

                if (factura.TipoFactura != "F")
                {
                    _contexto.SaveChanges();
                    return new Resultado(true, $"Se ha guardado exitosamente la orden con id {factura.IdFactura}");
                }

                var idMateriales = factura.Tbl_DetalleFactura.Select(x => x.IdMaterial);
                var materiales = _contexto.Tbl_Material.Where(x => idMateriales.Any(y => y == x.IdMaterial));

                foreach (var material in materiales)
                {
                    var detalle = factura.Tbl_DetalleFactura.FirstOrDefault(x => x.IdMaterial == material.IdMaterial);

                    material.Cantidad -= detalle.Cantidad;

                    if (detalle.Tbl_Material.Cantidad < 0)
                        return new Resultado(false, $"No hay stock disponible del producto {detalle.Tbl_Material.Descripcion}");

                    _contexto.Tbl_InventarioTracking.Add(new Tbl_InventarioTracking
                    {
                        IdMaterial = material.IdMaterial,
                        Cantidad = detalle.Cantidad,
                        Fecha = DateTime.Now,
                        IdUsuario = factura.IdUsuario,
                        IdMov = 2,
                        Observacion = $"Se registra salida por factura",
                    });

                    _contexto.Entry(material).State = EntityState.Modified;
                }

                _contexto.SaveChanges();

                return new Resultado(true, $"Se ha guardado exitosamente la factura con id {factura.IdFactura}");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }

        public Resultado AnularFactura(int id)
        {
            try
            {
                var factura = _contexto.Tbl_Factura.FirstOrDefault(x => x.IdFactura == id);

                if (factura is null)
                    return new Resultado(false, "No se ha encontrado la factura que desea anular");

                if (!factura.Activo)
                    return new Resultado(false, "La factura ya ha sido anulada");

                factura.Activo = false;

                if (factura.TipoFactura != "F")
                {
                    _contexto.SaveChanges();
                    return new Resultado(true, "Se ha anulado exitosamente la cotización");
                }

                foreach (var detalle in factura.Tbl_DetalleFactura)
                {

                    detalle.Tbl_Material.Cantidad += detalle.Cantidad;

                    detalle.Tbl_Material.Tbl_InventarioTracking.Add(new Tbl_InventarioTracking
                    {
                        Cantidad = detalle.Cantidad,
                        Fecha = DateTime.Now,
                        IdUsuario = factura.IdUsuario,
                        IdMov = 9,
                        Observacion = $"Se anula la factura #{factura.IdFactura}",
                    });

                    _contexto.Entry(detalle.Tbl_Material).State = EntityState.Modified;
                }

                _contexto.Entry(factura).State = EntityState.Modified;
                _contexto.SaveChanges();

                return new Resultado(true, "Se ha anulado exitosamente la factura");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }
    }
}
