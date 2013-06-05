using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace UrlHelper.Test
{
    [TestFixture]
    public class DefaultResoverTest
    {
        private DefaultResoverTestAppUrls AppUrls;

        [SetUp]
        public void SetUp()
        {
            ConfigurationManager.AppSettings["BaseUri"] = "http://testing.com";
            RouteTable.Routes.Clear();
            AppUrls = UrlManager<DefaultResoverTestAppUrls>.Initialize();
        }

        [Test]
        public void Should_be_able_To_resolve_array_of_ints()
        {
            Assert.AreEqual(new Uri("http://test.com/tests1/complete?test[0]=1&test[1]=2").PathAndQuery.ToLower(), AppUrls.Tests.Complete(new[] { 1, 2 }).ToString());
        }

        public class DefaultResoverTestAppUrls : AreaRegistration
        {
            public Tests1Urls Tests { get; set; }

            public override void RegisterArea(AreaRegistrationContext routes)
            {
                var namespaces = new[] { typeof(UrlManagerTest.SampleController).Namespace };

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

        public class Tests1Urls : UrlsBase
        {
            public Uri Complete(int[] ids)
            {
                var rvd = Action("Complete");
                rvd["test"] = ids;
                return ToUri(rvd);
            }
        }
    }
}
