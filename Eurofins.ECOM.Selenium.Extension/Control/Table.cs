using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/table")]
    public class Table : ControlBase
    {
        private string _columenBase;
        private string _rowBase;
        //Once the column is not in the original table, it need to input the by of new table
        private By _columnBy;

        public Table() { }

        /// <summary>
        /// Create a Table instance
        /// </summary>
        /// <param name="tableBy">Table Id</param>
        /// <param name="columnBy">Header Table Id, if equals to Table Id, set to null</param>
        /// <param name="exParts">1:Column base path, the define is '/thead/th', if not equal to this, need to give one. 2:Row base path, the define is '/tbody/tr', if not equal to this, need to give one</param>
        public Table(By tableBy, By columnBy, params string[] exParts)
            : base(tableBy)
        {
            _columnBy = columnBy == null ? tableBy : columnBy;
            this._columenBase = exParts[0];
            this._rowBase = exParts[1];
        }

        public Column Column
        {
            get
            {  
                if (_columenBase == null || _columenBase == "")
                    return new Column(_columnBy, "/thead/th");
                else
                    return new Column(_columnBy, _columenBase);
            }
        }

        public Rows Rows
        {
            get
            {
                IWebElement baseElement = base.WrappedElement;
                if (_rowBase == null || _rowBase == "")
                    return new Rows(baseElement, "/tbody/tr");                
                else
                    return new Rows(baseElement, _rowBase);
            }
        }

        public bool Hide
        {
            get
            {
                return GetAttribute("class") == "hide" ? true : false;
            }
        }

        public bool IsThisType
        {
            get
            {
                var typeString = ClassAttribute.Get(typeof (Table)).Replace("/", "");
                return WrappedElement.TagName == typeString;
            }
        }

        //public int RowCount
        //{
        //    get
        //    {
        //        int count = base.WrappedElement.FindElements(by: By.TagName("TR")).Count;
        //        return count;
        //    }
        //}
    }
}