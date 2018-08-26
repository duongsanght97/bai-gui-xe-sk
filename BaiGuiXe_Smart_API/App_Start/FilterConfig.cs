using System.Web;
using System.Web.Mvc;

namespace BaiGuiXe_Smart_API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
