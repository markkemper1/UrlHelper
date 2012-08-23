using System;
using System.Text;

namespace UrlHelper
{
    public static class UriExtensions
    {
        public static Uri ToHttps(this Uri uri)
        {
            if (uri.Scheme == "https")
                return uri;
            
            var sb = new StringBuilder();
            sb.Append("https://");
            sb.Append(uri.DnsSafeHost);

            if(uri.Port != 80)
            {
                sb.Append(":");
                sb.Append(uri.Port);
            }

            sb.Append(uri.PathAndQuery);

            return new Uri(sb.ToString());
        }
    }
}