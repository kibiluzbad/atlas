using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Atlas.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Root",
                "",
                new {controller = "Contatos", action = "Index"},
                new {httpMethod = new HttpMethodConstraint("GET")}
                );

            routes.MapRoute(
               "NewContato",
               "Contatos/New",
               new { controller = "Contatos", action = "New" },
               new { httpMethod = new HttpMethodConstraint("GET") }
               );

            routes.MapRoute(
               "CreateContato",
               "Contatos/",
               new { controller = "Contatos", action = "Create" },
               new { httpMethod = new HttpMethodConstraint("POST") }
               );

            routes.MapRoute(
             "ShowContato",
             "Contatos/{id}",
             new { controller = "Contatos", action = "Show" },
             new { httpMethod = new HttpMethodConstraint("POST"), id = "\\d+" }
             );
        }
    }
}