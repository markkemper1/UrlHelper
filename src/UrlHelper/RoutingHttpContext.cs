using System;
using System.Collections.Specialized;
using System.Web;

namespace UrlHelper
{
	public class RoutingHttpContext : HttpContextBase
	{
		private HttpResponse response;
		private HttpRequest request;

		public RoutingHttpContext(Uri baseUrl)
		{
			this.request = new HttpRequest(baseUrl);
			this.response = new HttpResponse(baseUrl);
		}

		public override HttpRequestBase Request
		{
			get { return this.request; }
		}

		public override HttpResponseBase Response
		{
			get { return this.response; }
		}

		public class HttpResponse : HttpResponseBase
		{
			private readonly Uri baseUri;

			public HttpResponse(Uri baseUri)
			{
				if (baseUri == null) throw new ArgumentNullException("baseUri");
				this.baseUri = baseUri;
			}

			public override string ApplyAppPathModifier(string virtualPath)
			{
				return VirtualPathUtility.Combine(baseUri.AbsolutePath, virtualPath);
			}
		}

		public class HttpRequest : HttpRequestBase
		{
			private readonly Uri baseUri;

			public HttpRequest(Uri baseUri)
			{
				if (baseUri == null) throw new ArgumentNullException("baseUri");
				this.baseUri = baseUri;
			}

			//public override Uri Url
			//{
			//    get
			//    {
			//        return this.baseUri;
			//    }
			//}

			public override string PathInfo
			{
				get { return baseUri.AbsolutePath; }
			}

			public override string AppRelativeCurrentExecutionFilePath
			{
				get { return "~/"; }
			}
			public override string ApplicationPath
			{
				get { return this.baseUri.AbsolutePath; }
			}

            public override NameValueCollection QueryString
            {
                get { return HttpUtility.ParseQueryString(baseUri.Query); }
            }
		}
	}
}