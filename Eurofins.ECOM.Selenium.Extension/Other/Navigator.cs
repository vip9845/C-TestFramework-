using Eurofins.ECOM.Selenium.Extension.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;


namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public class Navigator:IINavigator
    {
        private string location;
        private IBrowser _browser;
        private TimeSpan _pageLoadTimeout = new TimeSpan(0, 1, 0);
        private string _startPageUrl;

        private static Navigator instance = new Navigator();

        private Navigator() { }

        public static Navigator Instance
        {
            get
            {
                return instance;
            }
        }

        public void InitInstance(IBrowser browser, string startPage)
        {
            this._browser = browser;
            this._startPageUrl = startPage;
        }

        public string StartPageUrl
        {
            get { return _startPageUrl; }
        }

        public TimeSpan PageLoadTimeout
        {
            get { return _pageLoadTimeout; }
            set { _pageLoadTimeout = value; }
        }

        public IBrowser CurrentBrowser
        {
            get { return this._browser; }
        }

        public IList<IWebElement> GetAllElementsByXpath(string xPath)
        {
            return CurrentBrowser.WebDriver.FindElements(By.XPath(xPath));
        }

        public void SetWebDriverTimeout(int seconds)
        {
            CurrentBrowser.WebDriver.Manage().Timeouts().ImplicitWait=new TimeSpan(0, 0, seconds);//ImplicitlyWait(new TimeSpan(0, 0, seconds));
        }

        public int GetSecondsFromTimeSpan(TimeSpan timeSpan)
        {
            return timeSpan.Seconds + (timeSpan.Minutes * 60) + (timeSpan.Hours * 60 * 60);
        }

        public TT FirstOpen<TT>() where TT : IPage, new()
        {
            var target = new TT();
            CurrentBrowser.WebDriver.Navigate().GoToUrl(StartPageUrl);
            SetWebDriverTimeout(3);
            CurrentBrowser.MaximizeWindow();
            CurrentBrowser.WindowSelector.SetCurrentWindowHandel();
            CurrentBrowser.FrameSelector.ReFrame(target.PageFrameName);
            //WaitForTargetPageLoad(target.PageUrl);
            AssertErrorPage(target);
            SetWebDriverTimeout(GetSecondsFromTimeSpan(_pageLoadTimeout));
            return target;
        }

        public TT Navigate<TT>(Action action = null, string validatedPageUrl = null) where TT : IPage, new()
        {
            if (action != null)
                action();
            var target = new TT();
            DealwithModuleWindow();
            CurrentBrowser.WindowSelector.SetCurrentWindowHandel();
            CurrentBrowser.FrameSelector.ReFrame(target.PageFrameName);
            validatedPageUrl = validatedPageUrl == null ? target.PageUrl : validatedPageUrl;
            WaitForTargetPageLoad(validatedPageUrl);
            AssertErrorPage(target);
            return target;
        }

        public TT ClosePopUpAndReAttach<TT>(Action action = null, string validatedPageUrl = null) where TT : IPage, new()
        {
            if (action != null)
                action();
            var target = new TT();

            if (CurrentBrowser.WebDriver.WindowHandles.Count == 1)
                CurrentBrowser.WindowSelector.SwitchToWindowByWindowHandle(CurrentBrowser.WebDriver.WindowHandles[0].ToString());

            DealwithModuleWindow();
            CurrentBrowser.WindowSelector.SetCurrentWindowHandel();
            CurrentBrowser.FrameSelector.ReFrame(target.PageFrameName);
            validatedPageUrl = validatedPageUrl == null ? target.PageUrl : validatedPageUrl;
            WaitForTargetPageLoad(validatedPageUrl);
            AssertErrorPage(target);
            return target;
        }

        private void DealwithModuleWindow()
        {
            if (CurrentBrowser.JSAlert.IsPresent)
                CurrentBrowser.JSAlert.Accept();
        }

        public void WaitForAlertAndAccept()
        {
            IWebDriver driver = CurrentBrowser.WebDriver;
            int waitTime = int.Parse(EnvironmentManager.GetSettingValue("MinWaitTime")), i = 0;
            while (i < waitTime)
            {
                if (CurrentBrowser.JSAlert.IsPresent)
                {
                    CurrentBrowser.JSAlert.Accept();
                    break;
                }
                System.Threading.Thread.Sleep(1000);
                i++;
            }
        }

        public TT OpenPopup<TT>(Action action, string validatedPageUrl = null) where TT : IPage, new()
        {
            var target = new TT();
            CurrentBrowser.WindowSelector.SwitchToPopup(action);
            CurrentBrowser.FrameSelector.ReFrame(target.PageFrameName);
            validatedPageUrl = validatedPageUrl == null ? target.PageUrl : validatedPageUrl;
            WaitForTargetPageLoad(validatedPageUrl);
            //AssertErrorPage(target);
            return target;
        }

        private void WaitForTargetPageLoad(string targetPageUrl)
        {
            var waiter = new WebDriverWait(CurrentBrowser.WebDriver, PageLoadTimeout);
            waiter.Until(delegate(IWebDriver _driver)
            {
                if (CompareCurrentPageUrlToTarget(targetPageUrl))
                    return true;
                return false;
            });
        }

        private bool PageLoadWaiting(IWebDriver driver)
        {
            try
            {
                object result = ((IJavaScriptExecutor)driver).ExecuteScript("return document['readyState'] ? 'complete' == document.readyState : true");
                if (result != null && result is bool && (bool)result)
                    return true;
            }
            catch (Exception)
            {
                // Possible page reload. Fine
            }
            return false;
        }

        public TResult WaitForCondition<TResult>(Func<IWebDriver, TResult> condition, int defaultTimout = 5)
        {
            var timeout = TimeSpan.FromSeconds(defaultTimout);
            var waiter = new WebDriverWait(CurrentBrowser.WebDriver, PageLoadTimeout);
            return waiter.Until(condition);
        }

        private bool CompareCurrentPageUrlToTarget(string targetPageUrl)
        {
            string[] i_targetPageUrl = targetPageUrl.Split(',');

            for (int i = 0; i <= i_targetPageUrl.Length - 1; i++)//this is done to enable multiple page url's for a single page object/flow
            {
                if (CurrentBrowser.WindowSelector.CompareCurrentPageUrlToTarget(CompareSingleWindow, i_targetPageUrl[i]))
                {
                    return true;
                }
            }
            return false;
            //return CurrentBrowser.WindowSelector.CompareCurrentPageUrlToTarget(CompareSingleWindow, targetPageUrl);
        }

        private bool CompareSingleWindow(string targetPageUrl)
        {
            this.location = CurrentBrowser.PageSource.Contains(targetPageUrl).ToString();
            if (this.location == "True")
            {
                this.location = targetPageUrl;
            }

            var paramsStart = location.IndexOf('?');
            if (paramsStart >= 0)
            {
                location = location.Substring(0, paramsStart);
            }
            return location.ToLower().EndsWith(targetPageUrl.ToLower()) | location.ToLower().Contains(targetPageUrl.ToLower());//May not be perfect
        }

        private void AssertErrorPage<TT>(TT target) where TT : IPage, new()
        {
            SetWebDriverTimeout(3);
            var frames = CurrentBrowser.FrameSelector.Frames;
            SetWebDriverTimeout(GetSecondsFromTimeSpan(_pageLoadTimeout));
            //var iframes = CurrentBrowser.FrameSelector.IFrames;
            //frames.AddRange(iframes);
            if (frames.Count > 0)
            {
                foreach (var item in frames)
                {
                    try
                    {
                        var pageSource = CurrentBrowser.GetBodyText(item);
                        CompareWithErrorMessage(pageSource);
                        CurrentBrowser.FrameSelector.SwitchToDefaultContent();
                    }
                    catch { }
                }
            }
            else
            {
                try
                {
                    var bodyText = CurrentBrowser.GetBodyText();
                    CompareWithErrorMessage(bodyText);
                }
                catch { }
            }
        }

        private void CompareWithErrorMessage(string pageSource)
        {
            if (pageSource.Contains("Server Error in"))
                Assert.Fail("Server error while navigating\r\n\r\n {0}.", pageSource);

            if (pageSource.Contains("Internet Information Services") && pageSource.Contains("Microsoft Support"))
                Assert.Fail("IIS error while navigating\r\n\r\n {0}.", pageSource);
        }

        public void takeScreenShot(string fileName)
        {
            Screenshot screenshot = ((ITakesScreenshot)CurrentBrowser.WebDriver).GetScreenshot();
            screenshot.SaveAsFile(@fileName, ScreenshotImageFormat.Png);
        }
    }
}
