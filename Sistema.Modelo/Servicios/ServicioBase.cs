using Sistema.Datos.Contextos;

namespace Sistema.Modelo.Servicios
{
    public abstract class ServicioBase
    {
        protected readonly DeipsaContext _contexto;

        public ServicioBase(DeipsaContext contexto)
        {
            _contexto = contexto;
        }

        public ServicioBase()
        {
            _contexto = new DeipsaContext();
        }
    }
}
