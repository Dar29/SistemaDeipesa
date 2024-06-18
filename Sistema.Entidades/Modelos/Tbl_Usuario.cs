namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Usuario()
        {
            Tbl_Factura = new HashSet<Tbl_Factura>();
            Tbl_InventarioTracking = new HashSet<Tbl_InventarioTracking>();
            Tbl_OrdenCompra = new HashSet<Tbl_OrdenCompra>();
        }

        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(10)]
        public string Usuario { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(150)]
        public string Contrasenia { get; set; }

        public bool? Reestablecer { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaModificacion { get; set; }

        public bool? Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Factura> Tbl_Factura { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_InventarioTracking> Tbl_InventarioTracking { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_OrdenCompra> Tbl_OrdenCompra { get; set; }
    }
}
