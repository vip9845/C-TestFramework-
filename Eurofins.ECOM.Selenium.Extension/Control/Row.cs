using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/tr")]
    public class Row : ControlBase
    {
        //table/tbody/tr/td 
        //baseTableElment is the table element. 
        //
        private string _trBase;

        public Row() { }

        public Row(IWebElement webElement)
            : base(webElement)
        {
           
        }

        //public Row(IEBrowser browser, IWebElement baseTableElement, string trBase)
        //    : base(browser, baseTableElement)
        //{
        //    _trBase = trBase;
        //}

        public Cells Cells
        {
            get
            {
                return new Cells(base.WrappedElement, "/td");
            }
        }

        public int Count
        {
            get
            {
                return FindWebElementFromCurrentWebElement(_trBase + "/..").FindElements(By.TagName("tr")).Count;
            }
        }

        /// <summary>
        /// Select Cell by Attrubute (Some controls like row and table whose args of constructor is different from normal controls need to override this function)
        /// </summary>
        /// <typeparam name="TTControl">Sub Control Type</typeparam>
        /// <param name="name">Attribute Name</param>
        /// <param name="value">Attribute value</param>
        /// <returns></returns>
        public override TTControl ControlAttributeFuzzy<TTControl>(string name, string value)
        {
            if (typeof(TTControl) == this.GetType())
            {
                TTControl tt = new TTControl();              
                tt.WrappedElement = FindWebElementFromCurrentWebElement( _trBase + string.Format("[contains(@{0},'{1}')]", name, value));
                return tt;
            }
            else
            {
                TTControl tt = new TTControl();              
                tt.WrappedElement = FindWebElementFromCurrentWebElement(_trBase + ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(@{0},'{1}')]", name, value));
                return tt;
            }
        }
    }
}
