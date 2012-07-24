using System;

namespace UrlHelper
{
	public class RouteUri : Uri
	{
		public RouteUri(Uri baseUri, string virtualPath)
			: base(baseUri, virtualPath)
		{
		}

		public override string ToString()
		{
			return base.PathAndQuery;
		}
	}
}