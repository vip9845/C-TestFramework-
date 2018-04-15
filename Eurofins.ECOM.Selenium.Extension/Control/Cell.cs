using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/td")]
    public class Cell : ControlBase
    {
        //Need to give the element until "tr"

        private string _tdBase;

        public Cell() { }

        public Cell(IWebElement webElement)
            : base(webElement)
        {

        }

        public Cell(By by)
            : base(by)
        {
            
        }  

        public Cell this[int index]
        {
            get
            {
                string element = string.Format("[{0}]", index);
                base.WrappedElement = FindWebElementFromCurrentWebElement(this._tdBase + element);
                return this;
            }
        }

        public int RowIndex
        {
            get
            {
                return (int)this.Parent<Row>().Index;
            }
        }

        public override TTControl ControlText<TTControl>(string text, bool isSearchAllSubElement = false)
        {
            if (typeof(TTControl) == this.GetType())
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._tdBase + string.Format("[text()='{0}']", text), isSearchAllSubElement);
                return tt;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._tdBase + ClassAttribute.Get(typeof(TTControl)) + string.Format("[text()='{0}']", text), isSearchAllSubElement);
                return tt;
            }
        }

        public override TTControl ControlTextFuzzy<TTControl>(string text, bool isSearchAllSubElement = false)
        {
            if (typeof(TTControl) == this.GetType())
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._tdBase + string.Format("[contains(text(),'{0}')]", text),isSearchAllSubElement);
                return tt;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._tdBase + ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(text(),'{0}')]", text), isSearchAllSubElement);
                return tt;
            }
        }

        public override TTControl ControlAttributeFuzzy<TTControl>(string name, string value)
        {
            if (typeof(TTControl) == this.GetType())
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._tdBase + string.Format("[contains(@{0},'{1}')]", name, value));
                return tt;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._tdBase + ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(@{0},'{1}')]", name, value));
                return tt;
            }
        }
    }
}