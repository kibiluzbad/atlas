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
             "EditContato",
             "Contatos/{id}",
             new { controller = "Contatos", action = "Edit" },
             new { httpMethod = new HttpMethodConstraint("GET"), id = "\\d+" }
             );

            routes.MapRoute(
               "UpdateContato",
               "Contatos/{id}",
               new { controller = "Contatos", action = "Update" },
               new { httpMethod = new HttpMethodConstraint("POST"), id = "\\d+" }
               );

            routes.MapRoute(
              "DeleteContato",
              "Contatos/{id}",
              new { controller = "Contatos", action = "Delete" },
              new { httpMethod = new HttpMethodConstraint("DELETE"), id = "\\d+" }
              );

            routes.MapRoute(
              "ContatoAddPhone",
              "Contatos/{id}/AddPhone",
              new { controller = "Contatos", action = "AddPhone" },
              new { httpMethod = new HttpMethodConstraint("GET"), id = "\\d+" }
              );

            routes.MapRoute(
              "ContatoSavePhone",
              "Contatos/{id}/AddPhone",
              new { controller = "Contatos", action = "AddPhone" },
              new { httpMethod = new HttpMethodConstraint("POST"), id = "\\d+" }
              );
        }
    }
}