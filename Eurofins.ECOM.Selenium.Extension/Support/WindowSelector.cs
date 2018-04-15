//Copyright 2013 Eurofins Scientific Ltd, Ireland
//Usage reserved to Eurofins Global Franchise Model subscribers.

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ETFSeleniumUtility2.Support
{
    public class WindowSelector
    {
        private readonly string _originalWindowHandle;
        private readonly IWebDriver _driver;
        private readonly PopupWindowFinder _popupWindowFinder;
        public WindowSelector(IWebDriver driver)
        {
            _driver = driver;
            _originalWindowHandle = driver.CurrentWindowHandle;
            _popupWindowFinder = new PopupWindowFinder(driver);
        }
        public void SelectWindowByUrl(string windowUrl)
        {
            bool windowSuccessfullySwitched = false;
            string current = _originalWindowHandle;
            foreach (string handle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(handle);
                if (_driver.Url.Contains(windowUrl))
                {
                    windowSuccessfullySwitched = true;
                    break;
                }
            }

            if (!windowSuccessfullySwitched)
            {
                _driver.SwitchTo().Window(current);
                throw new WebDriverException("Unable to select window with URL: " + windowUrl);
            }
        }

        public void SelectNewPopupWindow(Action popUpMethod)
        {
            var newWindowId = _popupWindowFinder.Invoke(popUpMethod);
            _driver.SwitchTo().Window(newWindowId);
        }
        public void SelectTheOnlyWindow()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
        }
        public void CloseWindow()
        {
            var windowHandlesBeforeClose = _driver.WindowHandles.Count;
            _driver.Close();
            var waiter = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            waiter.Until(driver => driver.WindowHandles.Count == windowHandlesBeforeClose - 1);
            if (_driver.WindowHandles.Count == 1)
            {
                SelectTheOnlyWindow();
            }
        }

        public int WindowsCount
        {
            get
            {
                return _driver.WindowHandles.Count;
            }
        }
    }
}
