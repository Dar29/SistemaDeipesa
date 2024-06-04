namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_OrdenCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_OrdenCompra()
        {
            Tbl_DetalleOrdenCompra = new HashSet<Tbl_DetalleOrdenCompra>();
        }

        [Key]
        public int IdOrdenCompra { get; set; }

        public int IdProveedor { get; set; }

        public int IdEstadoOrden { get; set; }

        public DateTime FechaEmision { get; set; }

        public decimal? Impuesto { get; set; }

        public decimal? Descuento { get; set; }

        public decimal Total { get; set; }

        public int IdUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_DetalleOrdenCompra> Tbl_DetalleOrdenCompra { get; set; }

        public virtual Tbl_EstadoOrdenCompra Tbl_EstadoOrdenCompra { get; set; }

        public virtual Tbl_Proveedor Tbl_Proveedor { get; set; }

        public virtual Tbl_Usuario Tbl_Usuario { get; set; }
    }
}
