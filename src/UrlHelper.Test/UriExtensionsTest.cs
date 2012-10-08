using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace UrlHelper.Test
{
    [TestFixture]
    public class UriExtensionsTest
    {
        public class BaseUriWithPort
        {
            private SampleAppUrls AppUrls;

            [SetUp]
            public void SetUp()
            {
                RouteTable.Routes.Clear();
                ConfigurationManager.AppSettings["BaseUri"] = "http://testing.com:9818";
                AppUrls = UrlManager<SampleAppUrls>.Initialize(); 
            }

            [Test]
            public void should_keep_port_when_converting_to_https()
            {
                Assert.AreEqual("https://testing.com:9818/tests/edit/abc", AppUrls.Tests.Edit("abc").ToHttps().ToString());
            }

            [Test]
            public void should_keep_querystring_when_converting_to_https()
            {
                Assert.AreEqual("https://testing.com:9818/tests/complete?test=abc", AppUrls.Tests.Complete("abc").ToHttps().ToString());
            }
        }

        public class BaseUriNoPort
        {
            private SampleAppUrls AppUrls;

            [SetUp]
            public void SetUp()
            {
                RouteTable.Routes.Clear();
                ConfigurationManager.AppSettings["BaseUri"] = "http://testing.com";
                AppUrls = UrlManager<SampleAppUrls>.Initialize();
            }

            [Test]
            public void should_convert_to_https_easily()
            {
                Assert.AreEqual("https://testing.com/tests/edit/abc", AppUrls.Tests.Edit("abc").ToHttps().ToString());
            }


            [Test]
            public void should_keep_querystring_when_converting_to_https()
            {
                Assert.AreEqual("https://testing.com/tests/complete?test=abc", AppUrls.Tests.Complete("abc").ToHttps().ToString());
            }
        }


        // Test objects
        public class SampleAppUrls : AreaRegistration
        {
            public TestsUrls Tests { get; set; }

            public override void RegisterArea(AreaRegistrationContext routes)
            {
                var namespaces = new[] { typeof(SampleController).Namespace };

                routes.MapRoute("Standard", "{controller}/{action}/{id}",
                                new { controller = "Pages", action = "Index", Area = "public", id = string.Empty },
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
