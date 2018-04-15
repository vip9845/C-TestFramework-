using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/select")]
    public class DropDownList : SelectedControl
    {
        public DropDownList() { }

        public DropDownList(By by)
            : base(by)
        {

        }
    }
}