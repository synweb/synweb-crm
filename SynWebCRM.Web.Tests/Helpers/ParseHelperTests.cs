using System;
using System.Collections.Generic;
using System.Text;
using SynWebCRM.Web.Helpers;
using Xunit;

namespace SynWebCRM.Web.Tests.Helpers
{
    public class ParseHelperTests
    {
        [Theory]
        [InlineData("se12", "se12")]
        [InlineData("ro.dfse", "ro.dfse")]
        [InlineData("     http://vk.com/se12      ", "se12")]
        [InlineData("http://vk.com/se12", "se12")]
        [InlineData("https://vk.com/se12", "se12")]
        [InlineData("http://vk.com/se12/", "se12")]
        [InlineData("http://vk.com/ro.dfse/", "ro.dfse")]
        public void ParseVKTest(string str, string result)
        {
            var res = ParseHelper.ParseVK(str);
            Console.WriteLine(res);
            Assert.Equal(result, res);
        }


        [Theory]
        [InlineData("ro.dfse.")]
        [InlineData("12se")]
        [InlineData("http://vk.com/ro.dfse./")]

        public void ParseVKExceptionTest(string str)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var res = ParseHelper.ParseVK(str);
                Console.WriteLine(res);
            });
        }

        [Theory]
        [InlineData("se12", true)]
        [InlineData("ro.dfse", true)]
        [InlineData("ro.dfse.", false)]
        [InlineData("12se", false)]
        [InlineData("     http://vk.com/se12      ", true)]
        [InlineData("http://vk.com/se12", true)]
        [InlineData("https://vk.com/se12", true)]
        [InlineData("http://vk.com/se12/", true)]
        [InlineData("http://vk.com/ro.dfse/", true)]
        [InlineData("http://vk.com/ro.dfse./", false)]
        public void IsVKValidTest(string str, bool result)
        {
            var res = ParseHelper.IsVKValid(str);
            Console.WriteLine(res);
            Assert.Equal(result, res);
        }
    }
}
