using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/input")]
    public class TextField : ContainerControl
    {
        private string validatedType = "text";

        public TextField() { }

        public TextField(By by)
            : base(by)
        {
            
        }      
    }
}