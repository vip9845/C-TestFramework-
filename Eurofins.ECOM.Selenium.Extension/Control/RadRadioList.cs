
using Eurofins.ECOM.Selenium.Extension.Control;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class RadRadioList : ControlBase
    {
        public RadRadioList() { }

        public RadRadioList(By by)
            : base(by)
        {

        }

        public RadRadio this[string key]
        {
            get
            {
                IWebElement webElement = base.WrappedElement;

                return new RadRadio(webElement, key);
            }
        }
    }
}
