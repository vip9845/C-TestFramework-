using Eurofins.ECOM.Selenium.Extension.Other;
using Eurofins.Selenium.Extension.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Interface
{
    public interface IBrowser
    {
        IWebDriver WebDriver
        {
            get;
        }

        ActionEx Actions
        {
            get;
        }

        WindowSelector WindowSelector
        {
            get;
        }

        FrameSelector FrameSelector
        {
            get;
        }

        Alert JSAlert
        {
            get;
        }

        bool IsIE
        {
            get;
        }

        string PageSource
        {
            get;
        }

        string CurrentUrl
        {
            get;
        }

        string PageTitle
        {
            get;
        }

        string AppName
        {
            get;
        }

        string GetBodyText(IWebElement frameName = null);

        void Back();

        void Forward();

        void Refresh();

        void MaximizeWindow();
    }
}
