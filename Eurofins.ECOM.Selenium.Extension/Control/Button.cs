using Eurofins.Testing.Other;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/input[@type='button']")]
    public class Button:ControlBase
    {        
        public Button()
        {

        }

        public Button(By by):base(by)
        {

        }

        public void Click(Action ajaxAction = null)
        {
            base.Click();
            if (ajaxAction != null)
                ajaxAction();
        }
    }
}
