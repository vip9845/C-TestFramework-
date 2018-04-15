using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
namespace ETFSeleniumUtility2.Support
{
    public class Alert
    {
        private readonly IWebDriver _driver;
        public Alert(IWebDriver driver)
        {
            _driver = driver;
           
        }
        public void Accept()
        {
            _driver.SwitchTo().Alert().Accept();
        }
        public void Dismiss()
        {
            _driver.SwitchTo().Alert().Dismiss();
        }
        public void SendKeys(string keysToSend)
        {
            _driver.SwitchTo().Alert().SendKeys(keysToSend);
        }
        public string Text
        {
            get
            {
                return _driver.SwitchTo().Alert().Text;
            }
        }

        public bool IsPresent
        {
            get
            {
                try
                {
                     _driver.SwitchTo().Alert();
                    return true;
                }
                catch (NoAlertPresentException)
                {

                    return false;
                }
            }
        }

    }
}
