using Sistema.Datos.Contextos;
using Sistema.Entidades.Modelos;
using System.Web;

namespace Sistema.Modelo.Servicios
{
    public abstract class ServicioBase
    {
        protected readonly DeipsaContext _contexto;

        protected readonly Tbl_Usuario _usuario;

        public ServicioBase(DeipsaContext contexto)
        {
            _contexto = contexto;
            _usuario = HttpContext.Current.Session["__USER_SESSION__"] as Tbl_Usuario ?? null;
        }

        public ServicioBase()
        {
            _contexto = new DeipsaContext();
            _usuario = HttpContext.Current.Session["__USER_SESSION__"] as Tbl_Usuario ?? null;
        }
    }
}
