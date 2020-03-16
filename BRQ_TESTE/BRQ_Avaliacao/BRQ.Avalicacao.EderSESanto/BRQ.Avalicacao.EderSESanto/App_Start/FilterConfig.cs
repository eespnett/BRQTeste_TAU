using System.Web;
using System.Web.Mvc;

namespace BRQ.Avalicacao.EderSESanto
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
