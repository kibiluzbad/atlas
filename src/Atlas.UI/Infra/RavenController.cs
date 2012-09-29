using System;
using System.Web.Mvc;
using Raven.Client;

namespace Atlas.UI.Infra
{
    public abstract class RavenController : Controller
    {
        protected readonly IDocumentSession DocumentSession;

        protected bool IsAjaxRequest { get; private set; }

        protected ActionResult RespondTo(Func<ActionResult> normal, Func<ActionResult> ajax = null)
        {
            if (IsAjaxRequest) return ajax.Invoke();
            return normal.Invoke();
        }

        protected RavenController(IDocumentSession documentSession)
        {
            DocumentSession = documentSession;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] != null
                && filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                IsAjaxRequest = true;
            }
            else
            {
                IsAjaxRequest = false;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            DocumentSession.SaveChanges();
        }
    }
}