using System;

namespace WordCounter.Application.Common.Extensions
{
    public static class UrlDecoder
    {
        public static string DecodeUrlString(this string url) {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }
    }
}
