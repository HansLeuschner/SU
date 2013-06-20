using System.Web;
using System.Web.Mvc;

namespace Splintered_Universe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new Cumis.Filters.GlobalExceptionFilter());
            filters.Add(new HandleErrorAttribute());

        }
    }
}