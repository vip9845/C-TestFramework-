
using Eurofins.ECOM.Selenium.Extension.Control;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class TreeView : ControlBase
    {
        public TreeView() { }

        public TreeView(By by)
            : base(by)
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
    }
}