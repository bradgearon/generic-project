using System.Net.Http.Formatting;
using System.Web.Http;
using GenericProject.Web.App_Start;

namespace GenericProject.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Services.Replace(typeof (IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
    }
}