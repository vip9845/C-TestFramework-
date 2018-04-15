
using Eurofins.ECOM.Selenium.Extension.Control;
using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class RadComboBoxItem : ControlBase
    {
        private LoadingPanel _loadingPanel;
        public LoadingPanel LoadingPanel
        {
            get { return _loadingPanel; }
            set { _loadingPanel = value; }
        }

        public RadComboBoxItem(By by, By loadingPanel)
            : base(by)
        {
            if (loadingPanel != null)
                LoadingPanel = new LoadingPanel(loadingPanel);
            else LoadingPanel = null;
        }

        public RadComboBoxItem this[string key]
        {
            get
            {
                base.WrappedElement = FindWebElementFromCurrentWebElement("//li[contains(text(),'" + key + "')]");
                return this;
            }
        }

        public RadComboBoxItem this[int index]
        {
            get
            {
                base.WrappedElement = FindWebElementFromCurrentWebElement("//li[" + index + "]");
                return this;
            }
        }

        public void SelectItem()
        {
            base.Click();
            if (LoadingPanel != null)
                LoadingPanel.WaitLoadingVisible();
        }

        public bool ItemDisabled
        {
            get
            {
                return this.GetAttribute("class").ToString().Contains("rcbDisabled") ? true : false;
            }
        }
    }
}