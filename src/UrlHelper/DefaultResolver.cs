using System;
using System.Text;
using System.Web.Routing;

namespace UrlHelper
{
	internal class DefaultResolver : IResolver
	{
		private readonly RouteCollection routes;
		private readonly Uri baseUri;
		private RequestContext requestContext;

		public DefaultResolver(RouteCollection routes, Uri baseUri)
		{
			if (routes == null) throw new ArgumentNullException("routes");
			if (baseUri == null) throw new ArgumentNullException("baseUri");
			this.routes = routes;
			this.baseUri = baseUri;
			requestContext = new RequestContext(new RoutingHttpContext(baseUri), new RouteData());
		}

		public RouteCollection Routes
		{
			get { return routes; }
		}

		public Uri Resolve(RouteValueDictionary values)
		{
			if (!values.ContainsKey("area"))
				values["area"] = String.Empty;

			var result = Routes.GetVirtualPath(requestContext, values);

			if (result == null)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("Could not resolve url. Item Count: {0},  Item Values: ", values.Count);
				foreach (var item in values)
				{
					sb.AppendFormat("{0}={1}, ", item.Key, item.Value);
				}
				throw new ArgumentException(sb.ToString());
			}

			return new RouteUri(baseUri, result.VirtualPath);
		}
	}
}