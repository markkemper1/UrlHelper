using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlHelper
{
	public class UrlManager<AREA_TYPE> where AREA_TYPE : AreaRegistration, new()
	{
		public static AREA_TYPE Root { get; private set; }

		public static AREA_TYPE Initialize()
		{
			var baseUriString = ConfigurationManager.AppSettings["BaseUri"];

			if (String.IsNullOrEmpty(baseUriString))
				throw new ApplicationException("You must supply an appSetting called 'BaseUri'");

			return Initialize(RouteTable.Routes, new Uri(baseUriString));
		}

		public static AREA_TYPE Initialize(RouteCollection routes, Uri baseUri)
		{
			return Initialize(new DefaultResolver(routes, baseUri));
		}

		public static AREA_TYPE Initialize(IResolver resolver)
		{
			Root = new AREA_TYPE();
			SetResolver(resolver, Root);
			return Root;
		}

		public static void SetResolver<T>(IResolver resolver, T root) where T : new()
		{
			Stack<AreaRegistration> areas = new Stack<AreaRegistration>();

			SetResolver(resolver, root, null, areas);

			foreach (var area in areas)
			{
				area.RegisterArea(new AreaRegistrationContext(area.AreaName, resolver.Routes));
			}
		}

		internal static void SetResolver<T>(IResolver resolver, T root, string area, Stack<AreaRegistration> areaStack) where T : new()
		{
			Type type = root.GetType();

			if (root is AreaRegistration)
			{
				area = ((AreaRegistration)(object)root).AreaName;
				areaStack.Push(((AreaRegistration)(object)root));
			}

			if (root is IRouteResolver)
				((IRouteResolver)root).Initialize(resolver, area);

			var properties = type.GetProperties(BindingFlags.Public
												| BindingFlags.Instance
												| BindingFlags.SetProperty
												| BindingFlags.GetProperty
				);

			foreach (var property in properties)
			{
				if (!property.PropertyType.IsClass)
					continue;

				if (!property.PropertyType.GetConstructors().Any(x => x.GetParameters().Length == 0))
				{
					Trace.TraceWarning("No default constructor found for property " + property.Name + " on routing object: " + type.Name);
					continue;
				}


				object propertyObject;
				try
				{
					propertyObject = Activator.CreateInstance(property.PropertyType);
				}
				catch (MissingMethodException ex)
				{
					throw new ApplicationException("No default constructor found for : " + property.PropertyType, ex);
				}

				if (propertyObject is AreaRegistration)
					SetResolver<object>(resolver, propertyObject, area, areaStack);

				if (propertyObject is AreaRegistration)
					area = ((AreaRegistration)propertyObject).AreaName;

				if (propertyObject is IRouteResolver)
					((IRouteResolver)propertyObject).Initialize(resolver, area);

				property.SetValue(root, propertyObject, null);
			}
		}
	}

	public interface IRegisterRoutes
	{
		void Register(RouteCollection routes);
	}
}