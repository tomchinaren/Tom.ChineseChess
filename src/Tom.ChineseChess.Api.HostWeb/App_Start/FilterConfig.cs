using System.Web;
using System.Web.Mvc;

namespace Tom.ChineseChess.Api.HostWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
