using System.Web.Mvc;
using System.Web.Routing;

namespace event_hubs_sas_generator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Sas", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
