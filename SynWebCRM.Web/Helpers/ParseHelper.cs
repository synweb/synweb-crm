using System;
using System.Text.RegularExpressions;

namespace SynWebCRM.Web.Helpers
{
    public static class ParseHelper
    {
        public static string ParseVK(string vkString)
        {
            if (!IsVKValid(vkString))
            {
                throw new ArgumentException("Inalid link or id", nameof(vkString));
            }
            var trimmed = vkString.Trim();
            string idRegex = @"[a-zA-Z][a-zA-Z\d\._]*[a-zA-Z\d]";
            bool isSimple = Regex.IsMatch(trimmed, $"^{idRegex}$");
            if (isSimple)
                return trimmed;
            var linkMatch = Regex.Match(trimmed, $@"^https?://vk.com/({idRegex})/?$");
            if (linkMatch.Success && linkMatch.Groups.Count == 2)
                return linkMatch.Groups[1].Value;
            throw new ArgumentException("Inalid link or id", nameof(vkString));
        }

        public static bool IsVKValid(string vkString)
        {
            var trimmed = vkString.Trim();
            string idRegex = @"[a-zA-Z][a-zA-Z\d\._]*[a-zA-Z\d]";
            return Regex.IsMatch(trimmed, $@"^(https?://vk.com/)?{idRegex}/?$");
        }
    }
}