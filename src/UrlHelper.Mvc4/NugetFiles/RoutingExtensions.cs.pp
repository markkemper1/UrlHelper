using System;
using System.Web.Mvc;
using UrlHelper;

namespace $rootnamespace$.Routing
{
	public static class RoutingExtensions
	{
		public static AppUrls Urls(this Controller controller)
		{
			return UrlManager<AppUrls>.Root;
		}

		public static AppUrls Urls(this IViewDataContainer viewDataContainer)
		{
			return UrlManager<AppUrls>.Root;
		}
	}
}