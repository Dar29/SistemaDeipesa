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

        public int Cantidad { get; set; }

        public decimal Subtotal { get; set; }

        public virtual Tbl_Factura Tbl_Factura { get; set; }

        public virtual Tbl_Material Tbl_Material { get; set; }
    }
}
