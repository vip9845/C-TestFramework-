using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/div")]
    public class TreeViewChildren:ControlBase
    {
        public TreeViewChildren() { }

        public TreeViewChildren(IWebElement element)
            : base(element)
        {
           
        }

        public TreeViewItem TreeViewItem
        {
            get
            {
                TreeViewItem item = new TreeViewItem(base.WrappedElement);
                return item;
            }
        }

        public bool IsDisplayed
        {
            get
            {
                string attr = base.GetAttribute("style");             
                return attr.Contains("block") ? true : false;
            }
        }
    }
}
