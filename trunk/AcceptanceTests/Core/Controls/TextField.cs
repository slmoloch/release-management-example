using System;

using AcceptanceTests.Core.Controls;

using Selenium;

namespace Tests.SmokeTest.PageObjects.Controls
{
    public class TextField : ControlBase
    {
        private readonly string _selector;

        public TextField(ISelenium selenium, string selector)
            : base(selenium)
        {
            _selector = selector;
        }

        public void SetText(string text)
        {
            Selenium.Type(_selector, text);
        }

        public override String ToString()
        {
            return Selenium.GetValue(_selector);
        }
        
        public String GetSelector()
        {
            return _selector;
        }
    }
}