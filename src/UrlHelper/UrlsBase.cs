using System;
using System.Web.Routing;

namespace UrlHelper
{
    public class UrlsBase : UrlsEmptyBase
	{
		public virtual Uri Index()
		{
			return ActionUri("Index");
		}

		public virtual Uri Create()
		{
			return ActionUri("Create");
		}

		public virtual Uri Details(object id)
		{
			var rvd = Action("Details");
			rvd["id"] = id;
			return ToUri(rvd);
		}

		public virtual Uri Edit(object id)
		{
			var rvd = Action("Edit");
			rvd["id"] = id;
			return ToUri(rvd);
		}

		public virtual Uri Delete(object id)
		{
			var rvd = Action("Delete");
			rvd["id"] = id;
			return ToUri(rvd);
		}
	}
}