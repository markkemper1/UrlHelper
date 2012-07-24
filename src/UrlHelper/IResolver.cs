using System;
using System.Web.Routing;

namespace UrlHelper
{
	public interface IResolver
	{
		RouteCollection Routes { get; }
		Uri Resolve(RouteValueDictionary values);
	}
}