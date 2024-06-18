namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_TipoMovimientoInventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_TipoMovimientoInventario()
        {
            Tbl_InventarioTracking = new HashSet<Tbl_InventarioTracking>();
        }

        [Key]
        public int IdMov { get; set; }

        [Required]
        [StringLength(20)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_InventarioTracking> Tbl_InventarioTracking { get; set; }
    }
}
