using OpenQA.Selenium;

namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class ContainerControl : ControlBase
    {
        public ContainerControl() { }

        public ContainerControl(By by)
            : base(by)
        {
        }

        public string ContentText
        {
            set
            {
                base.Click();
                base.Clear();
                WrappedElement.SendKeys(value);
                CurrentBrowser.Actions.Scroll(-WrappedElement.Location.X, -WrappedElement.Location.Y);
            }
            get
            {
                return WrappedElement.Text;
            }
        }

        public void Paste()
        {
                WrappedElement.SendKeys(Keys.Control + 'v');
        }
    }
}
