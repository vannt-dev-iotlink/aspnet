using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    internal class RouteValueDictionnary : RouteValueDictionary
    {
        public RouteValueDictionnary(object values) : base(values)
        {
        }
    }
}