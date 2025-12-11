using System.Web.Mvc;
using System.Web.Routing;

namespace RailwayManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "BookingConfirmation",
                url: "Booking/BookingConfirmation/{pnr}",
                defaults: new { controller = "Booking", action = "BookingConfirmation", pnr = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
