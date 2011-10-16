using System.Threading;

using AcceptanceTests.Core;
using AcceptanceTests.Pages;

namespace AcceptanceTests.Flows
{
    public class HomePageFlow : FlowBase
    {
        private readonly HomePage home;

        public HomePageFlow(INavigator navigator, HomePage home) 
            : base(navigator)
        {
            this.home = home;
        }

        public HomePageFlow ShowEmailPopup()
        {
            home.SendMailLink.Click();
            Thread.Sleep(1000);

            return this;
        }

        public HomePageFlow FillBody(string body)
        {
            home.BodyText.SetText(body);

            return this;
        }

        public HomePageFlow ClickOnSend()
        {
            home.SendButton.Click();

            return this;
        }
    }
}
