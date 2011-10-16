using System;

using Selenium;

using Tests.SmokeTest.PageObjects.Controls;

namespace AcceptanceTests.Core.Controls
{
    public class Checkbox : ControlBase
    {
        private readonly string selector;

        public Checkbox(ISelenium selenium, string selector)
            : base(selenium)
        {
            this.selector = selector;
        }

        public bool Value
        {
            get
            {
                return Boolean.Parse(Selenium.GetValue(selector));
            }
            set
            {
                if (value)
                {
                    Selenium.Check(selector);
                }
                else
                {
                    Selenium.Uncheck(selector);
                }
            }
        }
        public void Click()
        {
            Selenium.Click(selector);
        }
    }
}