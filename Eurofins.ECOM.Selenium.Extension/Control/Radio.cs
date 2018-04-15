
using Eurofins.ECOM.Selenium.Extension.Control;
using Eurofins.Selenium.Extension.Other;
using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class Radio : ControlBase
    {
        public Radio() { }

        public Radio(By by)
            : base(by)
        {

        }

        public RadioItem GetRadioItemByText<TTControl>(string text) where TTControl : ControlBase, new()
        {
            if (base.WrappedElement == null)
            {
                var tt = new TTControl();
                tt.WrappedElement = tt.CurrentBrowser.WebDriver.FindElement(By.XPath("/" + string.Format("{0}", ClassAttribute.Get(typeof(TTControl)))));
                var t = new RadioItem(By.XPath("/" + ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(text(),'{0}')]", text)));
                return t;
            }
            else
            {
                var tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(string.Format("/{0}", ClassAttribute.Get(typeof(TTControl))));
                var t = tt.ControlTextFuzzy<TTControl>(text).PrecedingSibling<RadioItem>();
                return t;
            }
        }
    }
}

