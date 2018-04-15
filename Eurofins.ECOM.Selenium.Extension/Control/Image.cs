using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/input")]
    public class Image : ControlBase
    {
        public Image() { }

        public Image(By by)
            : base(by)
        {

        }

        public Image(IWebElement element)
            : base(element)
        {

        }
    }
}
