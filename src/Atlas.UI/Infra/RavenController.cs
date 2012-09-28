using System.Web.Mvc;
using Raven.Client;

namespace Atlas.UI.Infra
{
    public abstract class RavenController : Controller
    {
        protected readonly IDocumentSession DocumentSession;

        protected RavenController(IDocumentSession documentSession)
        {
            DocumentSession = documentSession;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            DocumentSession.SaveChanges();
        }
    }
}