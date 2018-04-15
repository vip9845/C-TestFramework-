using OpenQA.Selenium;
using System.Collections.Generic;


namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class OfferMenuItem : ControlBase
    {
        private By tableBase;
        private Queue<string> subItems;
        private string subItemId;
        public OfferMenuItem(By tableBase, Queue<string> subItems)
            : base(tableBase)
        {
            this.subItems = subItems;
            this.tableBase = tableBase;
            this.subItemId = subItems.Count > 0 ? subItems.Dequeue() : null;
        }

        public Table CurrentTable
        {
            get
            {
                return new Table(tableBase, null, "", "");
            }
        }

        public Table SubTable
        {
            get
            {
                return new Table(By.Id(subItemId),null, "", "");
            }
        }

        public Cell SubCell
        {
            get
            {
                return CurrentTable.Rows.Cells.ControlAttributeFuzzy<Cell>("id", subItemId);
            }
        }

        public void Action()
        {
            if (subItemId == null)
                return;
            Cell cell = CurrentTable.Rows.Cells.ControlAttributeFuzzy<Cell>("onclick", subItemId);
            if (!SubTable.IsPresent)
            {
                //cell.Click();
                IJavaScriptExecutor executor = (IJavaScriptExecutor)cell.CurrentBrowser.WebDriver;
                executor.ExecuteScript("arguments[0].click();", cell.WrappedElement);
            }
            else
            {
                while (SubTable.Hide)
                { cell.Click(); }

                string subTableBase = "//table[@id='" + subItemId + "']";
                OfferMenuItem omi = new OfferMenuItem(By.XPath(subTableBase), subItems);
                omi.Action();
            }
        }
    }
}
