using Eurofins.Testing.Other;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    [Class("/div")]
    public class TreeViewItem : ControlBase
    {
        public TreeViewItem()
        {
        }

        public TreeViewItem(IWebElement webElement)
            : base(webElement)
        {

        }

        public TreeViewItem this[string keyword]
        {
            get
            {
                return this.ControlText<Label>(keyword).Parent<TreeViewItem>();
            }
        }

        public TreeViewChildren TreeviewItemGroup
        {
            get
            {
                string treeviewGroupId = "G" + base.GetAttribute("id");
                TreeViewItem tviParent = base.Parent<TreeViewItem>();
                TreeViewChildren tvc = new TreeViewChildren(tviParent.FindWebElementFromCurrentWebElement("/div[@id='" + treeviewGroupId + "']"));
                if (!tvc.IsDisplayed)
                    Expand();
                return tvc;
            }
        }

        public void Expand()
        {
            string imgId = base.GetAttribute("id") + "c";         
            var img = base.ControlAttribute<Image>("id", imgId);           
            img.Click();
            //var img = new Image(WebDriver, By.XPath("//div[@id='tKey_tKey']/div/span[text()='Metals']/parent::div/img[contains(@style,'cursor: pointer;')]"));
        }

        public override TTControl ControlText<TTControl>(string text, bool isSearchAllSubElement = false)
        {
            TTControl tt = new TTControl();         
            tt.WrappedElement = FindWebElementFromCurrentWebElement("/div" + ClassAttribute.Get(typeof(TTControl)) + string.Format("[text()='{0}']", text),isSearchAllSubElement);
            return tt;
        }
    }
}
