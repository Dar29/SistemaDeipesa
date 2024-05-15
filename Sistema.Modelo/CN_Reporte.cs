using Sistema.Datos;
using Sistema.Entidades;
using System.Collections.Generic;

namespace Sistema.Modelo
{
    public class CN_Reporte
    {
        private CD_Reporte objSistemaDatos = new CD_Reporte();

        public List<CD_Reporte> ReporteInventarioGeneral(filtros obj, out string Mensaje)
        {
            return objSistemaDatos.ReporteInventarioGeneral(obj, out Mensaje);
        }
    }
}
