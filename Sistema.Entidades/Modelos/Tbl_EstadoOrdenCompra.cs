namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_EstadoOrdenCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_EstadoOrdenCompra()
        {
            Tbl_OrdenCompra = new HashSet<Tbl_OrdenCompra>();
        }

        [Key]
        public int IdEstadoOrden { get; set; }

        [Required]
        [StringLength(20)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_OrdenCompra> Tbl_OrdenCompra { get; set; }
    }
}
