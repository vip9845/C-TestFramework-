using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/span")]
    public class Label : ContainerControl
    {
        public Label() { }  

        public Label(By by)
            : base(by)
        {
          
        }
    }
}