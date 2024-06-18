using Sistema.Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Modelo.Servicios
{
    public class TipoPagosServicio : ServicioBase
    {
        public  TipoPagosServicio() : base() { }

        public IEnumerable<Tbl_TipoPagos> ObtenerTodos()
        {
            _contexto.Configuration.ProxyCreationEnabled = false;
            return _contexto.Tbl_TipoPagos.ToList();
        }
    }
}
