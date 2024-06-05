using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Sistema.Web.Filters
{
    public class SessionFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        private IEnumerable<string> _rutasPublicas = new List<string>()
        {
            "Seguridad/IniciarSesion"
        };

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var session = HttpContext.Current.Session["__USER_SESSION__"];

            if (session != null) return;

            var rutas = filterContext.RequestContext.RouteData.Values;

            rutas.TryGetValue("controller", out object objControlador);
            rutas.TryGetValue("action", out object objAccion);

            string controlador = (string)objControlador,
                accion = (string)objAccion;

            var esRutaPublica = _rutasPublicas.Any(x => x.Split('/')[0] == controlador && x.Split('/')[1] == accion);

            if (esRutaPublica) return;

            //if (controlador != "Home" || accion != "Index") 
            //{
            //    filterContext.Result = new HttpUnauthorizedResult();

            //    return;
            //}

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
            {
                { "controller", "Seguridad" },
                { "action", "IniciarSesion" },
            });
        }

    }
}