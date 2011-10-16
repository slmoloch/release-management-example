using Selenium;

namespace AcceptanceTests.Core.Controls
{
    public class ControlBase
    {
        private readonly ISelenium selenium;

        public ControlBase(ISelenium selenium)
        {
            this.selenium = selenium;
        }

        protected ISelenium Selenium
        {
            get
            {
                return selenium;
            }
        }
    }
}