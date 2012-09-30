using System;
using System.Web.Mvc;
using NLog;

namespace Atlas.UI.Infra
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class NLogExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var logger = LogManager.GetLogger(filterContext.Controller.GetType().Name);

            logger.FatalException(filterContext.Exception.Message,filterContext.Exception);
            filterContext.ExceptionHandled = false;
        }
    }
}