using Eurofins.ECOM.Selenium.Extension.Control;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class RadRadio : ControlBase
    {
        public RadRadio(IWebElement webElement, string keyText)
            : base(webElement)
        {
            base.WrappedElement = FindWebElementFromCurrentWebElement("/input[@id='" + GetIdByText(keyText) + "']");
        }

        private string GetIdByText(string keyText)
        {
            return FindWebElementFromCurrentWebElement("//span[contains(text(),'" + keyText + "')]/parent::label").GetAttribute("for").ToString();
        }

        public bool IsSelected
        {
            get
            {
                return (GetAttribute("checked") == null) ? false : true;
            }
        }
    }
}
