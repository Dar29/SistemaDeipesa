namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Factura()
        {
            Tbl_DetalleFactura = new HashSet<Tbl_DetalleFactura>();
        }

        [Key]
        public int IdFactura { get; set; }

        public int IdCliente { get; set; }

        public DateTime FechaEmision { get; set; }

        public decimal? Impuesto { get; set; }

        public decimal? Descuento { get; set; }

        public decimal Total { get; set; }

        public DateTime Fecha { get; set; }

        public int IdTipoPagos { get; set; }

        public bool Activo { get; set; }

        [StringLength(1)]
        public string TipoFactura { get; set; }

        public int IdUsuario { get; set; }

        public virtual Tbl_Cliente Tbl_Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_DetalleFactura> Tbl_DetalleFactura { get; set; }

        public virtual Tbl_TipoPagos Tbl_TipoPagos { get; set; }

        public virtual Tbl_Usuario Tbl_Usuario { get; set; }
    }
}
