using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/div")]
    public class Div : ControlBase
    {
        public Div() { }

        public Div(By by)
            : base(by)
        {

        }
    }
}
