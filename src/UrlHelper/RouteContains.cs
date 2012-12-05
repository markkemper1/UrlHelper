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

        public bool CaseSensitive { get; set; }

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
		    var routeValue = values[parameterName];

		    foreach (var item in items)
		    {
		        if (item.Equals(routeValue))
		            return true;


		        if (!CaseSensitive && item is string && routeValue is string)
		            return String.Compare((string) item, (string) routeValue, StringComparison.InvariantCultureIgnoreCase) ==
		                   0;
		    }
		    return false;
		}
	}
}