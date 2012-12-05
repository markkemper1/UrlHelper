using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using NUnit.Framework;

namespace UrlHelper.Test
{
    [TestFixture]
    public class RouteContainsTest
    {
        [TestCase("Ship", "Ship")]
        [TestCase("1", "1")]
        [TestCase("SHip", "shIP")]
        public void should_match(object input, object testValue)
        {
            var target = new RouteContains(input);

            var result = target.Match(null, null, "test", new RouteValueDictionary()
                                                 {
                                                     {"test", testValue}
                                                 }, RouteDirection.UrlGeneration)
            ;


            Assert.IsTrue(result, "Input: " + input + " TestValue: " + testValue);
        }

        [TestCase("Ship1", "Ship")]
        public void should_not_match(object input, object testValue)
        {
            var target = new RouteContains(input);

            var result = target.Match(null, null, "test", new RouteValueDictionary()
                                                 {
                                                     {"test", testValue}
                                                 }, RouteDirection.UrlGeneration)
            ;


            Assert.IsFalse(result, "Input: " + input + " TestValue: " + testValue);
        }
    }
}
