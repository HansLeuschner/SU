using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using Cumis.Controllers;
//using Cumis.Filters;

namespace Splintered_Universe
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Login",
                action = "Login",
                id = UrlParameter.Optional
            });

           
        }
      }
}