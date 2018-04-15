using Eurofins.Genomics.MIDI.PageObjects;
using Eurofins.ECOM.Selenium.Extension.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Eurofins.PageObjects.Pages
{
    public class HomePage: PageBase
    {
        public HomePage():base (""){}

        internal TextField searchText
        {
         get
            {
                return Get<TextField>(By.XPath(".//*[@id='lst-ib']"));
            }
        }

        internal Button searchButton
        {
            get
            {
                return Get<Button>(By.Name("btnK"));
            }
        }
    }
}
