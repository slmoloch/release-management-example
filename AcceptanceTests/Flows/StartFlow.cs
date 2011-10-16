using System;

using AcceptanceTests.Core;
using AcceptanceTests.Pages;

namespace AcceptanceTests.Flows
{
    public class StartFlow : FlowBase, IDisposable
    {
        public StartFlow(INavigator navigator)
            : base(navigator)
        { }

        public HomePageFlow GoToHomePage()
        {
             var homePage = Navigator.Open<HomePage>();

             return new HomePageFlow(Navigator, homePage);
        }

        public void Dispose()
        {
            Navigator.Dispose();
        }
    }
}
