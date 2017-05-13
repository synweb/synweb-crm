using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynWebCRM.Web.Helpers;

namespace SynWebCRM.Tests
{
    [TestClass]
    public class VoidTests
    {
        [TestMethod]
        [DataRow("se12", "se12")]
        [DataRow("ro.dfse", "ro.dfse")]
        
        [DataRow("     http://vk.com/se12      ", "se12")]
        [DataRow("http://vk.com/se12", "se12")]
        [DataRow("https://vk.com/se12", "se12")]
        [DataRow("http://vk.com/se12/", "se12")]
        [DataRow("http://vk.com/ro.dfse/", "ro.dfse")]

        public void ParseVKTest(string str, string result)
        {
            var res = ParseHelper.ParseVK(str);
            Console.WriteLine(res);
            Assert.AreEqual(result, res);
        }

        [TestMethod]
        [DataRow("ro.dfse.")]
        [DataRow("12se")]
        [DataRow("http://vk.com/ro.dfse./")]

        public void ParseVKExceptionTest(string str)
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var res = ParseHelper.ParseVK(str);
                Console.WriteLine(res);
            });
        }

        [TestMethod]
        [DataRow("se12", true)]
        [DataRow("ro.dfse", true)]
        [DataRow("ro.dfse.", false)]
        [DataRow("12se", false)]
        [DataRow("     http://vk.com/se12      ", true)]
        [DataRow("http://vk.com/se12", true)]
        [DataRow("https://vk.com/se12", true)]
        [DataRow("http://vk.com/se12/", true)]
        [DataRow("http://vk.com/ro.dfse/", true)]
        [DataRow("http://vk.com/ro.dfse./", false)]
        public void IsVKValidTest(string str, bool result)
        {
            var res = ParseHelper.IsVKValid(str);
            Console.WriteLine(res);
            Assert.AreEqual(result,res);
        }

        [TestMethod]
        public void UselessTest()
        {
            Assert.IsTrue(true);
        }
    }
}
