using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace UrlHelper.Test
{
	[TestFixture]
	public class UrlManagerTest
	{
		private SampleAppUrls AppUrls;
 
		[SetUp]
		public void SetUp()
		{
			ConfigurationManager.AppSettings["BaseUri"] = "http://testing.com";
            RouteTable.Routes.Clear();
			AppUrls = UrlManager<SampleAppUrls>.Initialize();
		}

		[Test]
		public void should_return_full_absoluteUri()
		{
			Assert.AreEqual("http://testing.com/tests", AppUrls.Tests.Index().AbsoluteUri);
		}

		[Test]
		public void should_return_relative_path_on_ToString()
		{
			Assert.AreEqual("/tests", AppUrls.Tests.Index().ToString());
		}

		[Test]
		public void should_add_unknown_parameters_to_query_string()
		{
			Assert.AreEqual("/tests/complete?test=abc", AppUrls.Tests.Complete("abc").ToString());
		}

		[Test]
		public void should_add_known_parameters_to_url_segment()
		{
			Assert.AreEqual("/tests/edit/abc", AppUrls.Tests.Edit("abc").ToString());
		}

		public class SampleAppUrls : AreaRegistration
		{
			public TestsUrls Tests { get; set; }

			public override void RegisterArea(AreaRegistrationContext routes)
			{
				var namespaces = new[] {typeof (SampleController).Namespace};

				routes.MapRoute("Standard", "{controller}/{action}/{id}",
				                new {controller = "Pages", action = "Index", Area = "public", id = string.Empty},
				                namespaces
					);
			}

			public override string AreaName
			{
				get { return "Public"; }
			}
		}

		public class TestsUrls : UrlsBase
		{
			public Uri Complete(object test)
			{
				var rvd = Action("Complete");
				rvd["test"] = test;
				return ToUri(rvd);
			}
		}

		//Dummy controller to show use of namespace in route registration
		public class SampleController : Controller
		{
		}
	}
}
