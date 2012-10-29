using System;
using System.Collections;
using System.Collections.Generic;
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

            if (items.Length == 1 && (items[0] is IEnumerable<object>))
            {
                items = ((IEnumerable<object>) items[0]).ToArray();
            }
		    this.items = items;
		}

		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return items.Contains(values[parameterName]);
		}
	}
}