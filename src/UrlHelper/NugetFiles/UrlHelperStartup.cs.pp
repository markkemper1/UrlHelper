using System;
using UrlHelper;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.Routing.UrlHelperStartup), "Start")]
namespace $rootnamespace$.Routing
{
	public static class UrlHelperStartup
	{
		public static void Start()
		{
			UrlManager<AppUrls>.Initialize();
		}
	}
}