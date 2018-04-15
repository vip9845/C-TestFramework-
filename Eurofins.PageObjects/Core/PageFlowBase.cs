using System;
using System.Configuration;
using Eurofins.Selenium.Extension.Other;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eurofins.ECOM.Selenium.Extension.Other;

namespace Eurofins.Genomics.MIDI.PageObjects.Core
{
    public class PageFlowBase : FlowBase
    {
        private int waitTime = -1;

        private readonly PageBase _pageBaseInstance;

        public PageBase PageBaseInstance
        {
            get
            {
                return _pageBaseInstance;
            }
        }

        public PageFlowBase() { }

        public PageFlowBase(PageBase page)
        {
            _pageBaseInstance = page;
        }

        public int TimeOut
        {
            get
            {
                return 5;
            }
        }

        public int WaitTime
        {
            get
            {
                if (waitTime == -1)
                {
                    try
                    {
                        waitTime = int.Parse(ConfigurationManager.AppSettings["WaitTime"].ToString());
                        if (waitTime < 1)
                            waitTime = 0;
                    }
                    catch(Exception ex)
                    {
                        waitTime = 0;
                    }
                }

                return waitTime;
            }
        }

        public void Pause(int second)
        {
            if (second <= 0)
                return;
            System.Threading.Thread.Sleep(second * 1000);
        }

        public void WaitMoment()
        {
            Pause(WaitTime);
        }

        public void WaitMoment(int timeInSeconds)
        {
            Pause(timeInSeconds);
        }

        public void WaitForControlToExist(IWebElement element)
        {

            //var waiter = new WebDriverWait(CurrentBrowser.WebDriver, waitTime);
            //waiter.Until(delegate(IWebDriver _driver)
            //{
            //    if (CompareCurrentPageUrlToTarget(targetPageUrl))
            //        return true;
            //    return false;
            //});

            bool elementExists = false;
            int i = 0;
            while (!elementExists)
            {
                try
                {
                    if (element.Enabled && element.Displayed)
                        return;
                    else
                        throw new NoSuchElementException();
                }
                catch(Exception ex)
                {
                    elementExists = false;
                    Pause(1);
                    if (i > WaitTime)
                    {
                        throw new NoSuchElementException();
                    }
                    i++;
                }
            }

        }

        public void WaitForJQueryToFinish()
        {
            ETFSeleniumUtility2.Support.JSWaiter.WaitForJQueryAjaxFinished(Navigator.Instance.CurrentBrowser.WebDriver, 30);
        }

        public void HoldOn()
        {
            Pause(100000000);
        }

        public void WindowMaximize()
        {
            Navigator.Instance.CurrentBrowser.MaximizeWindow();
        }

        public void ClosePageDirectly()
        {
            Navigator.Instance.CurrentBrowser.WindowSelector.CloseWindowAndCheck();
        }

        public void WaitForPageClose()
        {
            Navigator.Instance.CurrentBrowser.WindowSelector.CloseCheck();
        }

        public bool CheckAlertPanel(string key)
        {
            if (Navigator.Instance.CurrentBrowser.JSAlert.Text.Contains(key))
                return true;
            return false;
        }
        
        public TT ClickAlertPanel<TT>(string checkedKey = "") where TT : PageFlowBase, new()
        {
            try
            {
                if (Navigator.Instance.CurrentBrowser.JSAlert.Text.Contains(checkedKey))
                    Navigator.Instance.CurrentBrowser.JSAlert.Accept();
            }
            catch (NullReferenceException ex)
            {
                //throw ex;
                Assert.Fail("Getting Null Reference value");
            }
            catch(Exception ex)
            {
                //throw ex;
                Assert.Fail("Getting Null Reference value");
            }
            return this as TT;
        }

        public void ClickAlertPanel(string checkedKey = "")
        {
            try
            {
                if (Navigator.Instance.CurrentBrowser.JSAlert.Text.Contains(checkedKey))
                    Navigator.Instance.CurrentBrowser.JSAlert.Accept();
            }
            catch (NullReferenceException ex)
            {
                //throw ex;
                Assert.Fail("Getting Null Reference value");
            }
            catch (Exception ex)
            {
                //throw ex;
                Assert.Fail("Getting Null Reference value");
            }
        }

        public void GoTo(string url)
        {
            Navigator.Instance.CurrentBrowser.WebDriver.Navigate().GoToUrl(url);
        }

        public void CloseFeedbackPopUp()
        {
            try
            {
                if (Navigator.Instance.CurrentBrowser.WebDriver.FindElements(By.Id("k_popup")).Count > 0)
                {
                    Navigator.Instance.CurrentBrowser.WebDriver.FindElement(By.Id("k_pop_no_btn")).Click();
                }
            }catch(NotFoundException e)
            {
                throw e;
            }

        }

        public string CurrentPageUrl
        {
            get
            {
                return Navigator.Instance.CurrentBrowser.WebDriver.Url;
            }
        }
        public void WaitForModalHeaderToVanish()
        {
            try
            {
                while (Navigator.Instance.CurrentBrowser.WebDriver.FindElement(By.XPath("//div[@class='modal-header']")).Displayed)
                {
                    WaitMoment(10);
                }
            }
            catch (NoSuchElementException ex)
            {
                throw ex;
            }
        }
        public void WaitForLoadingIconToVanish()
        {
            // WaitMoment();
            try
            {
                while (Navigator.Instance.CurrentBrowser.WebDriver.FindElement(By.XPath("//div[@id='progressWithoutCancel']")).Displayed)
                {
                    WaitMoment(10);
                }
            }
            catch (NoSuchElementException ex)
            {
                throw ex;
            }
        }
    }
}