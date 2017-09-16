using System.Web;
using System.Web.Mvc;

namespace Journly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); //restrict access to the web app to logged in users. redirects anon users to login page.
        }
    }
}
