using System.Collections.Generic;
using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/tr")]
    public class Rows : ControlBase
    {
        //table/tbody/tr/td 
        //baseTableElment is the table element. 
        //
        private string _trBase;

        public Rows() { }

        public Rows(IWebElement baseTableElement, string trBase)
            : base(baseTableElement)
        {
            _trBase = trBase;         
        }

        public Row this[int index]
        {
            get
            {
                string element = string.Format("[{0}]", index);
                IWebElement webElement = FindWebElementFromCurrentWebElement(this._trBase + element);
                return new Row(webElement);
            }
        }

        public Cells Cells
        {
            get
            {
                return new Cells(base.WrappedElement, this._trBase + "/td/div");
            }
        }

        public int Count
        {
            get
            {
                return FindWebElementFromCurrentWebElement(this._trBase + "/..").FindElements(By.TagName("tr")).Count;
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
                this._trBase = this._trBase + string.Format("[contains(@{0},'{1}')]", name, value);
                return this as TTControl;
            }
            else if (typeof(TTControl) == typeof(Row))
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._trBase + string.Format("[contains(@{0},'{1}')]", name, value));
                return tt;
            }
            else
            {
                TTControl tt = new TTControl();
                tt.WrappedElement = FindWebElementFromCurrentWebElement(this._trBase + ClassAttribute.Get(typeof(TTControl)) + string.Format("[contains(@{0},'{1}')]", name, value));
                return tt;
            }
        }

        public override IList<TTControl> GetControlCollection<TTControl>()
        {
            IList<TTControl> controlCollection = new List<TTControl>();
            int i = 1;
            do
            {
                try
                {
                    var tt = new TTControl();
                    tt.WrappedElement = FindWebElementFromCurrentWebElement(this._trBase + "[" + i.ToString() + "]");
                    if (tt.WrappedElement == null)
                        break;
                    controlCollection.Add(tt);
                    i++;
                }
                catch (NotFoundException nx) { break;}
            } while (true);

            return controlCollection;
        }
    }
}
