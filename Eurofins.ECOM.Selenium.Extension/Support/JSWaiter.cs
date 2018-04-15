// Copyright 2013 Eurofins Scientific Ltd, Ireland
// Usage reserved to Eurofins Global Franchise Model subscribers.

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ETFSeleniumUtility2.Support
{
    public static class JSWaiter
    {
        private  static bool JSEval(IWebDriver driver,string script)
        {
            object result = ((IJavaScriptExecutor)driver).ExecuteScript(script);
            if (result == null)
            {
                return false;
            }
            else
            {
                return (bool)result;
            }
        }

        private static void JSWait(IWebDriver driver,string script, TimeSpan timeout)
        {
            var waiter = new WebDriverWait(driver, timeout);
            waiter.Until(d => JSEval(driver, script));
        }
        
        public static void ClickAndWaitForMicrosoftAjaxFinished(IWebDriver driver, Action click)
        {
            click();
            WaitForMicrosoftAjaxFinished(driver);
        }

        public static void WaitForJQueryAjaxFinished(IWebDriver driver,int timeout=10)
        {
            JSWait(driver,"return jQuery.active == 0", new TimeSpan(0,0,timeout));
        }
        public static void WaitForMicrosoftAjaxFinished(IWebDriver driver, int timeout=10)
        {
            JSWait(driver, "return !(Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack());", new TimeSpan(0, 0, timeout));
        }
    }
}
