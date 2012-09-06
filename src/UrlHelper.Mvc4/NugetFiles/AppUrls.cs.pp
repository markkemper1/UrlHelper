using System;
using UrlHelper;
using System.Web.Mvc;
using $rootnamespace$.Controllers;

namespace $rootnamespace$.Routing
{
	public class AppUrls : AreaRegistration
	{
		public AccountsUrls Accounts { get; set; }

		public override void RegisterArea(AreaRegistrationContext routes)
		{
			// Specify a controller namespace when using areas.
			var namespaces = new string[] { /* typeof(PagesController).Namespace */ };

			/*
			routes.MapRoute("StaticPages", "{viewName}", new { controller = "Pages", action = "Index", Area = "public", viewName = "{viewName}" },
							new { viewName = new RouteContains("Privacy", "Advertise", "About", "Terms") }, namespaces);
			*/

			routes.MapRoute("Login", "Login", new { controller = "Accounts", Action = "Login", Area = "public" }, namespaces);

			routes.MapRoute("Standard", "{controller}/{action}/{id}",
							new { controller = "Pages", action = "Index", Area = "public", id = string.Empty },
							namespaces
							);
		}

		public override string AreaName
		{
			get { return "public"; }
		}
	}
}