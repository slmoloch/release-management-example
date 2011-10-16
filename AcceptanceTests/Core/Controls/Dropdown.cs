using AcceptanceTests.Core.Controls;

using Selenium;

namespace Tests.SmokeTest.PageObjects.Controls
{
    public class Dropdown : ControlBase
    {
        private readonly string _selector;

        public Dropdown(ISelenium selenium, string selector)
            : base(selenium)
        {
            _selector = selector;
        }

        public string[] Values
        {
            get
            {
                return Selenium.GetSelectOptions(_selector);
            }
        }
        public string Value
        {
            get
            {
                return Selenium.GetSelectedLabel(_selector);
            }
            set
            {
                Selenium.Select(_selector, "label=" + value);
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}