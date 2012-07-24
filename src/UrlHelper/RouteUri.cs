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

	public static class UriExtensions
	{
		public static string ToHttps(this Uri uri)
		{
			if (uri.Scheme == "https")
				return uri.AbsoluteUri;

			return String.Format("https://{0}{1}", uri.DnsSafeHost, uri.AbsolutePath);
		}
	}
}