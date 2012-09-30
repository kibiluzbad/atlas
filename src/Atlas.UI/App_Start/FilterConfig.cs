using System.Web;
using System.Web.Mvc;
using Atlas.UI.Infra;

namespace Atlas.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NLogExceptionFilterAttribute());
        }
    }
}