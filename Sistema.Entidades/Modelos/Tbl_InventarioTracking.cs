namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_InventarioTracking
    {
        [Key]
        public int IdTracking { get; set; }

        public int IdMov { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

        [StringLength(1000)]
        public string Observacion { get; set; }

        public int IdUsuario { get; set; }

        public int IdMaterial { get; set; }

        public virtual Tbl_TipoMovimientoInventario Tbl_TipoMovimientoInventario { get; set; }

        public virtual Tbl_Material Tbl_Material { get; set; }

        public virtual Tbl_Usuario Tbl_Usuario { get; set; }
    }
}
