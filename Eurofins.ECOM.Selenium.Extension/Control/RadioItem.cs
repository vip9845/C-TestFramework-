using Eurofins.Selenium.Extension.Other;
using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/input[@type='radio']")]
    public class RadioItem : CheckBox
    {
        public RadioItem() { }

        public RadioItem(By by)
            : base(by)
        {

        }
        
        //public bool Checked
        //{
        //    get
        //    {
        //        return IsChecked;
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            for (int i = 0; i < 10; i++)
        //            {
        //                if (!IsChecked)
        //                {
        //                    base.Click();
        //                }
        //                System.Threading.Thread.Sleep(500);
        //            }
        //        }
        //        else
        //        {
        //            while (IsChecked)
        //                base.Click();
        //        }
        //    }
        //}

        //private bool IsChecked
        //{
        //    get
        //    {
        //        return GetAttribute("checked") == "true" ? true : false;
        //    }
        //}        
    }
}