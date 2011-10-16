using AcceptanceTests.Core.Controls;

using Selenium;

namespace Tests.SmokeTest.PageObjects.Controls
{
    public class Link : ControlBase
    {
        private readonly string _selector;

        public Link(ISelenium selenium, string selector)
            : base(selenium)
        {
            _selector = selector;
        }

        public void Click()
        {
            Selenium.Click(_selector);
        }
    }
}