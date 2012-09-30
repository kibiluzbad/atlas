using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Atlas.UI.Infra
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString Highlight<TModel>(this HtmlHelper<TModel> helper, string value, string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return new HtmlString(value);

            value = HttpUtility.HtmlEncode(value);
            term = HttpUtility.HtmlEncode(term);

            return helper.Raw(Regex.Replace(value,term,(match) => string.Format("<span class=\"highlight\">{0}</span>", match.Value),RegexOptions.CultureInvariant | RegexOptions.IgnoreCase ));
        }
    }
}