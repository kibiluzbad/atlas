using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Atlas.UI.Infra;
using AutoMapper;
using Autofac;
using Autofac.Integration.Mvc;
using NLog;

namespace Atlas.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RegisterContainer();

            RegisterMaps();

            _logger.Info("Aplicação iniciada.");
        }

        protected void Application_End()
        {
            _logger.Info("Aplicação finalizada.");
        }

        private static void RegisterMaps()
        {
            Mapper.AddProfile<ContatoProfile>();
            Mapper.AddProfile<TelefoneProfile>();
        }

        private static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}