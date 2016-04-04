using System;
using System.Collections;
using NUnit.Framework;
using SynWebCRM.Helpers;

namespace SynWebCRM.Tests.Helpers
{
    [TestFixture]
    public class ParseHelperTests
    {
        [Test]
        [TestCase("se12", "se12")]
        [TestCase("ro.dfse", "ro.dfse")]
        [TestCase("ro.dfse.", "", ExpectedException = typeof(ArgumentException))]
        [TestCase("12se", "", ExpectedException = typeof(ArgumentException))]
        [TestCase("     http://vk.com/se12      ", "se12")]
        [TestCase("http://vk.com/se12", "se12")]
        [TestCase("https://vk.com/se12", "se12")]
        [TestCase("http://vk.com/se12/", "se12")]
        [TestCase("http://vk.com/ro.dfse/", "ro.dfse")]
        [TestCase("http://vk.com/ro.dfse./", "", ExpectedException = typeof(ArgumentException))]

        public void ParseVKTest(string str, string result)
        {
            var res = ParseHelper.ParseVK(str);
            Console.WriteLine(res);
            Assert.That(result.Equals(res));
        }

        [TestCase("se12", true)]
        [TestCase("ro.dfse", true)]
        [TestCase("ro.dfse.", false)]
        [TestCase("12se", false)]
        [TestCase("     http://vk.com/se12      ", true)]
        [TestCase("http://vk.com/se12", true)]
        [TestCase("https://vk.com/se12", true)]
        [TestCase("http://vk.com/se12/", true)]
        [TestCase("http://vk.com/ro.dfse/", true)]
        [TestCase("http://vk.com/ro.dfse./", false)]
        public void IsVKValidTest(string str, bool result)
        {
            var res = ParseHelper.IsVKValid(str);
            Console.WriteLine(res);
            Assert.That(result.Equals(res));
        }
    }
}
