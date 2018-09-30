using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mvcweb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "ProDetail",
             url: "chi-tiet/{ProName}/{id}",
             defaults: new { controller = "Products", action = "Details",ProName= UrlParameter.Optional, id = UrlParameter.Optional },
                 new[] { "mvcweb.Controllers" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    new[] { "mvcweb.Controllers" });
             
        }
    }
}
