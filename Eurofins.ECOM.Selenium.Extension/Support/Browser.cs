using System;
using OpenQA.Selenium;

namespace ETFSeleniumUtility2.Support
{
    public class Browser
    {
        private readonly IWebDriver _driver;
        private readonly IJavaScriptExecutor _scriptExecutor;
        private bool _isIE;
        private bool _isIE9;

        public IJavaScriptExecutor ScriptExecutor
        {
            get
            {
                return _scriptExecutor;
            }
        }
        private string GetAppName()
        {
            var appName = _scriptExecutor.ExecuteScript("return navigator.appName");
            return appName.ToString();
        }
        public bool IsIE
        {
            get
            {
                return _isIE;
            }
        }
        private bool CheckIfIE9()
        {
            var ua = ((IJavaScriptExecutor)_driver).ExecuteScript("return navigator.userAgent.indexOf('Trident/5')>-1");
            var isIE9 = Convert.ToBoolean(ua.ToString());
            return isIE9;

        }
        internal Browser(IWebDriver driver)
        {
            this._driver = driver;
            _scriptExecutor = (driver as IJavaScriptExecutor);
            _isIE = (GetAppName() == "Microsoft Internet Explorer");
            _isIE9 = IsIE & CheckIfIE9();
        }

        public bool IsIE9 
        {
            get
            {
                return _isIE9;
            }

        }
        public void WindowMaximize()
        {
            if (!_driver.GetType().Name.Equals("ChromeDriver"))
              _driver.Manage().Window.Maximize();
 
        }
        public string PageSource
        {
            get
            {
                return _driver.PageSource;
            }
        }
        public string CurrentUrl
        {
            get
            {
                return _driver.Url;
            }
        }
        public string PageTitle
        {
            get
            {
                return _driver.Title;
            }
        }
        public void Back()
        {
            _driver.Navigate().Back();
        }
        public void Forward()
        {
            _driver.Navigate().Forward();
        }
        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }
        public Alert Alert
        {
            get
            {
                return new Alert(_driver);
            }
        }
        public OpenQA.Selenium.Interactions.Actions Actions
        {
            get
            {
                return new OpenQA.Selenium.Interactions.Actions(_driver);
            }
        }


    }
}
