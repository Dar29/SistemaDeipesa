namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Material()
        {
            Tbl_DetalleFactura = new HashSet<Tbl_DetalleFactura>();
            Tbl_DetalleOrdenCompra = new HashSet<Tbl_DetalleOrdenCompra>();
            Tbl_InventarioTracking = new HashSet<Tbl_InventarioTracking>();
        }

        [Key]
        public int IdMaterial { get; set; }

        public int IdEstadoMaterial { get; set; }

        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaModificacion { get; set; }

        public int IdMoneda { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public virtual Tbl_Categoria Tbl_Categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_DetalleFactura> Tbl_DetalleFactura { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_DetalleOrdenCompra> Tbl_DetalleOrdenCompra { get; set; }

        public virtual Tbl_EstadoMaterial Tbl_EstadoMaterial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_InventarioTracking> Tbl_InventarioTracking { get; set; }

        public virtual Tbl_Moneda Tbl_Moneda { get; set; }
    }
}
