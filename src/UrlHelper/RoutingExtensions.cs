using System;
using System.Web.Mvc;

namespace UrlHelper
{
	public static class UrlManagerExtensionss
	{
		public static RedirectResult ToRedirect(this Uri uri, bool permanent = false)
		{
			return new RedirectResult(uri.ToString(), permanent);
		}
	}
}