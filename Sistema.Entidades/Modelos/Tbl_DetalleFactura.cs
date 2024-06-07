namespace Sistema.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_DetalleFactura
    {
        [Key]
        public int IdDetalleFactura { get; set; }

        public int IdFactura { get; set; }

        public int IdMaterial { get; set; }

        public decimal Cantidad { get; set; }

        public decimal Subtotal { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Tbl_Factura Tbl_Factura { get; set; }

        public virtual Tbl_Factura Tbl_Factura1 { get; set; }

        public virtual Tbl_Material Tbl_Material { get; set; }

        public virtual Tbl_Material Tbl_Material1 { get; set; }
    }
}
