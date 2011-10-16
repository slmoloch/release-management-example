using System;
using System.IO;

using NUnit.Framework;

using Selenium;

namespace AcceptanceTests.Core
{
    public class Navigator : INavigator
    {
        private const string Timeout = "20000";
        private ISelenium _selenium;
        private string _siteUrl;

        public TT Open<TT>() where TT : PageBase, new()
        {
            var target = new TT();
            InitPage(target);

            _selenium.Open(Path.Combine(_siteUrl, target.PageUrl));

            WaitLoad(target);
            AssertErrorPage(target);
            AssertCorrectPageLoaded(target);
            return target;
        }

        public TT Navigate<TT>(Action action) where TT : PageBase, new()
        {
            return Navigate<TT>(action, false);
        }

        public void ClickAndWaitForText(Action action, Func<string> getSelector)
        {
            action();
            WaitForText(getSelector);
        }

        public void ClickAndWaitForElement(Action action, Func<string> getSelector)
        {
            action();
            WaitElementIsPresent(getSelector);
        } 
        
        public void ClickAndWaitForJQuery(Action action)
        {
            action();
            WaitForJQuery();
        }

        private void WaitLoad<TT>(TT target) where TT : PageBase
        {
            _selenium.WaitForPageToLoad(Timeout);
        }

        public void WaitForText(Func<string> selector)
        {
            WaitForCondition("var value = selenium.getText('{0}'); value.length > 0;", selector());
        }

        public void WaitElementIsPresent(Func<string> selector)
        {
            WaitForCondition("selenium.isElementPresent('{0}');", selector());
        } 
        
        public void WaitForJQuery()
        {
            _selenium.WaitForCondition("selenium.browserbot.getCurrentWindow().jQuery.active == 0;", Timeout);
        }

        private void WaitForCondition(string condition, string selector)
        {
            string script = string.Format(condition, JsEncode(selector));

            _selenium.WaitForCondition(script, Timeout);
        }

        private void AssertCorrectPageLoaded<TT>(TT target) where TT : PageBase
        {
            var location = _selenium.GetLocation();
            var paramsStart = location.IndexOf('?');

            if (paramsStart >= 0)
            {
                location = location.Substring(0, paramsStart);
            }

            if (!location.EndsWith(target.PageUrl))
            {
                Assert.Fail("Expected URL {0} but was {1}", target.PageUrl, location);
            }
        }

        private static string JsEncode(string selector)
        {
            return selector.Replace(@"'", @"\'");
        }

        public TT Navigate<TT>(Action action, bool chooseOkOnConfirmation) where TT : PageBase, new()
        {
            var target = new TT();
            InitPage(target);

            if (chooseOkOnConfirmation) _selenium.ChooseOkOnNextConfirmation();

            action();

            if (chooseOkOnConfirmation)
            {
                if (_selenium.IsConfirmationPresent())
                {
                    _selenium.GetConfirmation();
                }
            }

            WaitLoad(target);
            AssertErrorPage(target);
            AssertCorrectPageLoaded(target);

            return target;
        }

        private static void AssertErrorPage<TT>(TT target) where TT : PageBase, new()
        {
            var bodyText = target.Selenium.GetBodyText();
            if (bodyText.Contains("Server Error in "))
            {
                Assert.Fail("Server error while navigating\r\n\r\n {0}.", bodyText);
            }

            if (bodyText.Contains("Internet Information Services") && bodyText.Contains("Microsoft Support"))
            {
                Assert.Fail("IIS error while navigating\r\n\r\n {0}.", bodyText);
            }
        }

        private void InitPage<TT>(TT target) where TT : PageBase
        {
            target.Selenium = _selenium;
        }

        public void Start(string siteUrl)
        {
            _siteUrl = siteUrl;

            _selenium = new DefaultSelenium(Configuration.SeleniumHost, Configuration.SeleniumPort, "*firefox", _siteUrl);
            _selenium.Start();
            _selenium.SetTimeout(Timeout);
        }

        public void Dispose()
        {
            _selenium.Stop();
        }
    }
}