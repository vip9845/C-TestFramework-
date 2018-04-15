using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Eurofins.ECOM.Selenium.Extension.Interface;
using Eurofins.ECOM.Selenium.Extension.Other;

namespace Eurofins.Selenium.Extension.Other
{
    public class FirefoxBrowser : IBrowser
    {
        private readonly IWebDriver _webDriver;
        private ActionEx _action;
        private readonly IJavaScriptExecutor _scriptExecutor;
        private WindowSelector _windowSelector;
        private FrameSelector _frameSelector;

        public FirefoxBrowser(IWebDriver driver)
        {
            this._webDriver = driver;
            _scriptExecutor = (driver as IJavaScriptExecutor);
        }

        public Alert JSAlert
        {
            get
            {
                return new Alert(_webDriver);
            }
        }

        public WindowSelector WindowSelector
        {
            get { return _windowSelector ?? (_windowSelector = new WindowSelector(_webDriver)); }
        }

        public FrameSelector FrameSelector
        {
            get { return _frameSelector ?? (_frameSelector = new FrameSelector(_webDriver)); }
        }

        public IWebDriver WebDriver
        {
            get
            {
                return this._webDriver;
            }
        }
        public ActionEx Actions
        {
            get { return _action ?? (_action = new ActionEx(this._webDriver)); }
        }

        public bool IsIE
        {
            get
            {
                return AppName == "Mozilla firefox";
            }
        }

        public string PageSource
        {
            get
            {
                return _webDriver.PageSource;
            }
        }

        public string CurrentUrl
        {
            get
            {
                return _webDriver.Url;
            }
        }

        public string PageTitle
        {
            get
            {
                return _webDriver.Title;
            }
        }

        public string AppName
        {
            get
            {
                return _scriptExecutor.ExecuteScript("return navigator.appName").ToString();
            }
        }

        public string GetBodyText(IWebElement frameName = null)
        {
            return frameName == null ? _webDriver.FindElement(By.TagName("body")).Text : _webDriver.SwitchTo().Frame(frameName).FindElement(By.TagName("body")).Text;
        }

        public void Back()
        {
            _webDriver.Navigate().Back();
        }

        public void Forward()
        {
            _webDriver.Navigate().Forward();
        }

        public void Refresh()
        {
            _webDriver.Navigate().Refresh();
        }

        public void MaximizeWindow()
        {
            _webDriver.Manage().Window.Maximize();
            //_webDriver.Manage().Window.Position = new Point(0, 0);
            //((IJavaScriptExecutor)_webDriver).ExecuteScript("window.resizeTo(" + Screen.PrimaryScreen.WorkingArea.Width + ", " + Screen.PrimaryScreen.WorkingArea.Height + ");");
          
        }

        ///// <summary>
        ///// JQuery
        ///// </summary>
        ///// <returns></returns>
        //public bool ActiveAjaxConnections()
        //{
        //    return Actions.InvokeScript<bool>("jQuery.active == 0");
        //}
 
        ///// <summary>
        ///// MS AJAX
        ///// </summary>
        ///// <returns></returns>
        //public bool ActiveAjaxConnections()
        //{
        //    return Actions.InvokeScript<bool>("Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack()");
        //}

        ///// <summary>
        ///// Prototype
        ///// </summary>
        ///// <returns></returns>
        //public bool ActiveAjaxConnections()
        //{
        //    return Actions.InvokeScript<bool>("Ajax.activeRequestCount == 0");
        //}  

        // /// <summary>
        // /// Dojo
        // /// </summary>
        // /// <returns></returns>
        //public bool ActiveAjaxConnections()
        //{
        //    return Actions.InvokeScript<bool>("dojo.io.XMLHTTPTransport.inFlight.length == 0");
        //}

    }
}
