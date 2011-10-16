using Selenium;

namespace AcceptanceTests.Core
{
    public class PageBase
    {
        public ISelenium Selenium { get; set; }
        private readonly string _pageUrl;

        public PageBase(string pageUrl)
        {
            _pageUrl = FormatUrl(pageUrl);
        }

        public string PageUrl
        {
            get { return _pageUrl; }
        }

        private static string FormatUrl(string pageUrl)
        {
            var url = pageUrl;

            if (url.StartsWith("~/"))
            {
                url = url.Remove(0, 2);
            }
            return url;
        }
    }
}