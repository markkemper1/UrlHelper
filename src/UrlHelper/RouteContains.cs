using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace UrlHelper
{
	public class RouteContains : IRouteConstraint
	{
		private readonly object[] items;

		public RouteContains(params object[] items)
		{
			if (items == null) throw new ArgumentNullException("items");
			this.items = items;
		}

		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return items.Contains(values[parameterName]);
		}
	}
}