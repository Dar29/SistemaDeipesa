namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Inventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Inventario()
        {
            Tbl_InventarioTracking = new HashSet<Tbl_InventarioTracking>();
        }

        [Key]
        public int IdInventario { get; set; }

        public int IdMaterial { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaEntrada { get; set; }

        public int Cantidad { get; set; }

        [StringLength(150)]
        public string Observacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_InventarioTracking> Tbl_InventarioTracking { get; set; }

        public virtual Tbl_Material Tbl_Material { get; set; }
    }
}
