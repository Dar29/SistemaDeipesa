using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class OrdenCompra
    {
        //        CREATE TABLE Tbl_OrdenCompra(
        //    IdOrdenCompra INT IDENTITY (1,1) PRIMARY KEY,
        //    IdProveedor INT NOT NULL,
        //	FechaEmision DATETIME NOT NULL,
        //    Impuesto DECIMAL(18,2) NULL,
        //	Descuento DECIMAL(18,2) NULL,
        //	Total DECIMAL(18,2) NOT NULL,
        //    FOREIGN KEY(IdProveedor) REFERENCES Tbl_Proveedor(IdProveedor)
        //)
        public int IdOrdenCompra { get; set; }
        public Proveedor IdProveedor { get; set; }
        public EstadoOrdenCompra IdEstado { get; set; }
        public DateTime FechaEmision { get; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }


    }
}
