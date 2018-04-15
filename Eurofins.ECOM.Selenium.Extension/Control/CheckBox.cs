using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/input[@type='checkbox']")]
    public class CheckBox : ControlBase
    {
        public CheckBox() { }

        public CheckBox(By by)
            : base(by)
        {

        }

        public bool Checked
        {
            get
            {
                return IsChecked;
            }
            set
            {
                for (int i = 0; i < 10; i++)
                {
                    if (IsChecked == value)
                    { 
                        return; 
                    }
                    else
                    { 
                        base.Click(); 
                    }
                    System.Threading.Thread.Sleep(500);
                }

                //if (value)
                //{
                //    for (int i = 0; i < 10; i++)
                //    {
                //        if (!IsChecked)
                //        { base.Click(); }
                //        else
                //        { return; }
                //        System.Threading.Thread.Sleep(500);
                //    }
                //}
                //else
                //{
                //    while (IsChecked)
                //        base.Click();
                //}
            }
        }

        private bool IsChecked
        {
            get
            {
                return GetAttribute("checked") == "true" ? true : false;
            }
        }        
    }
}