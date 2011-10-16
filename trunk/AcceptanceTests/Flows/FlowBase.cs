using AcceptanceTests.Core;

namespace AcceptanceTests.Flows
{
    public class FlowBase
    {
        private readonly INavigator navigator;

        public FlowBase(INavigator navigator)
        {
            this.navigator = navigator;
        }

        public INavigator Navigator
        {
            get { return navigator; }
        }
    }
}