using System.Web.Http;
namespace iasset.FlightDashBoard 
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{id}", new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            );
        }
    }
}
