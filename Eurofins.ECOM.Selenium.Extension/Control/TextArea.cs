using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/textarea")]
    public class TextArea : ContainerControl
    {
        public TextArea() { }

        public TextArea(By by)
            : base(by)
        {

        }
    }
}