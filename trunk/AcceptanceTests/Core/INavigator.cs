using System;

namespace AcceptanceTests.Core
{
    public interface INavigator : IDisposable
    {
        TT Open<TT>() where TT : PageBase, new();
        TT Navigate<TT>(Action action) where TT : PageBase, new();

        void ClickAndWaitForText(Action action, Func<string> control);
        void ClickAndWaitForElement(Action action, Func<string> control);
        void ClickAndWaitForJQuery(Action action);

        void WaitForText(Func<string> selector);
        void WaitElementIsPresent(Func<string> selector);
        void WaitForJQuery();
    }
}