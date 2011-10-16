using AcceptanceTests.Flows;

namespace AcceptanceTests.Core
{
    public class TestBase
    {
        public StartFlow GetStart()
        {
            var navigator = new Navigator();
            navigator.Start(Configuration.SiteUrl);

            return new StartFlow(navigator);
        }
    }
}