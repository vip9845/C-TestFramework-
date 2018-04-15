using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/a")]
    public class LinkField : ControlBase
    {
        public LinkField() { }

        public LinkField(By by)
            : base(by)
        {

        }

        public string Href
        {
            get
            {
                return base.GetAttribute("href");
            }
        }
    }
}