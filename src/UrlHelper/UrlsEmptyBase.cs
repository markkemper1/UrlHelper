using System;
using System.Web.Routing;

namespace UrlHelper
{
	public class UrlsEmptyBase : IRouteResolver
	{
		protected IResolver resolver;
		protected string className;
		protected string area;

		public void Initialize(IResolver resolver, string area)
		{
			if (resolver == null) throw new ArgumentNullException("resolver");
			if (area == null) throw new ArgumentNullException("area");
			this.area = area;
			this.resolver = resolver;
		    this.className = this.GetType().Name.Replace("Urls", String.Empty);

            if(!KeepCase)
                this.className = className.ToLower();
		}

        public virtual bool KeepCase { get; set; }


		protected Uri ToUri(RouteValueDictionary rvd)
		{
			return this.resolver.Resolve(rvd);
		}

		public virtual RouteValueDictionary Action(string actionName)
		{
			var rvd = new RouteValueDictionary();
			rvd.Add("Area", area);
			rvd.Add("Controller", className);
			rvd.Add("Action", actionName);
			return rvd;
		}

		public virtual Uri ActionUri(string actionName)
		{
			return this.resolver.Resolve(this.Action(actionName));
		}
	}
}