using System;
using System.Collections;
using System.Text;
using System.Web.Routing;

namespace UrlHelper
{
    internal class DefaultResolver : IResolver
    {
        private readonly RouteCollection routes;
        private readonly Uri baseUri;
        private RequestContext requestContext;
        public static bool ForceLowerCase = true;

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

            var resolveable = new RouteValueDictionary();
            foreach (var item in values)
            {
                if (item.Value is IEnumerable && !(item.Value is string))
                {
                    IEnumerable forEachAble = (IEnumerable) item.Value;
                    int i = 0;
                    foreach (var x in forEachAble)
                    {
                        resolveable.Add(item.Key + "[" + (i++) +"]", x);
                    }
                }
                else
                    resolveable.Add(item.Key, item.Value);
            }


            var result = Routes.GetVirtualPath(requestContext, resolveable);

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

            var path = ForceLowerCase && result.VirtualPath != null ? result.VirtualPath.ToLowerInvariant() : result.VirtualPath;

            return new RouteUri(baseUri, path);
        }
    }
}