
using Eurofins.Selenium.Extension.Other;
using OpenQA.Selenium;
using Eurofins.Testing.Other;
using Eurofins.ECOM.Selenium.Extension.Control;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class QuotationMenu : ControlBase
    {
        public QuotationMenu(By by)
            : base(by)
        {

        }

        //public QuotationMenuItem this[QuotationMenuPages key]
        //{
        //    get
        //    {
        //        string[] itemPara = FieldAttribute.Gets(key);            
        //        return new QuotationMenuItem(BuildSelector(itemPara[0],itemPara[2]));
        //    }
        //}

        public IWebElement BuildSelector(string parentMenuItem,string menuItemUrl)
        {
            IWebElement webElement;
            //div[@id='ctl00_radMenu']/ul/li/a/span[contains(text(),'Modify / Display')]/parent::a /div/ul/li           
            if (parentMenuItem == "")
            {
                webElement = FindWebElementFromCurrentWebElement("/ul/li");
            }
            else
            {
                var webElementMenuItem = FindWebElementFromCurrentWebElement("/ul/li/a/span[contains(text(),'" + parentMenuItem + "')]/parent::a");
                base.CurrentBrowser.Actions.MouseHoverOn(webElementMenuItem);
                System.Threading.Thread.Sleep(1000);
                webElementMenuItem.Click();
                System.Threading.Thread.Sleep(1000);
                webElement = FindWebElementFromCurrentWebElement("/ul/li/div/ul/li/a[contains(@href,'" + menuItemUrl + "')]");
            }
            return webElement;
        }
    }
}