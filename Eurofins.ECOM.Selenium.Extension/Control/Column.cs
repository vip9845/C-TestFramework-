using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/th")]
    public class Column : ControlBase
    {
        //Need to give the element until "th"  
        private string _thBase;

        public Column() { }

        public Column(By baseTableBy, string thBase)
            : base(baseTableBy)
        {
            _thBase = thBase;
        }

        public Column this[int index]
        {
            get
            {
                string element = _thBase + string.Format("[{0}]", index);
                base.WrappedElement = FindWebElementFromCurrentWebElement(element);
                return this;
            }
        }

        public override TTControl ControlText<TTControl>(string text, bool isSearchAllSubElement = false)
        {
            if (typeof(TTControl) == this.GetType())
            {
                WrappedElement = FindWebElementFromCurrentWebElement(_thBase + string.Format("[text()='{0}']", text), isSearchAllSubElement);
                return this as TTControl;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(_thBase + ClassAttribute.Get(typeof(TTControl)) + string.Format("[text()='{0}']", text), isSearchAllSubElement);
                return tt;
            }
        }

        public override TTControl Control<TTControl>(bool isSearchAllSubElement = false)
        {
            TTControl tt = new TTControl();
            tt.WrappedElement = FindWebElementFromCurrentWebElement(_thBase + ClassAttribute.Get(typeof(TTControl)), isSearchAllSubElement);
            return tt;
        }
    }
}
