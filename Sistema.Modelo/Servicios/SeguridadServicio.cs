using Sistema.Entidades.Utils;
using Sistema.Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sistema.Modelo.Servicios
{
    public class SeguridadServicio : ServicioBase
    {
        public SeguridadServicio() { }

        public Resultado IniciarSesion(string usuario, string contraseña)
        {
            try
            {
                var entidad = _contexto.Tbl_Usuario.FirstOrDefault(x => x.Usuario.Trim() == usuario.Trim().ToUpper() && (x.Activo ?? false));

                if (entidad is null)
                    return new Resultado(false, "El usuario no ha sido encontrado");

                var esValida = Encriptador.Verificar(contraseña, entidad.Contrasenia);

                if (!esValida)
                    return new Resultado(false, "Contraseña incorrecta, intente nuevamente");

                HttpContext.Current.Session.Remove("__USER_SESSION__");
                HttpContext.Current.Session["__USER_SESSION__"] = entidad;

                return new Resultado(true, "Se ha iniciado sesión correctamente");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }

        public Resultado CerrarSesion()
        {
            try
            {
                HttpContext.Current.Session["__USER_SESSION__"] = null;

                return new Resultado(true, "Se ha cerrado sesión correctamente");
            }
            catch (Exception ex)
            {
                return new Resultado(false, ex.Message);
            }
        }
    }
}
