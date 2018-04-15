using OpenQA.Selenium;

namespace ETFSeleniumUtility2.Support
{
    public class Helper
    {
        private readonly IWebDriver _driver;
        private readonly Browser _browser;
        private readonly FrameSelector _frameSelector;
        public Helper(IWebDriver driver)
        {
            this._driver = driver;
            _browser = new Browser(_driver);
            _frameSelector = new FrameSelector(_driver);

        }

        public Browser Browser
        {
            get
            {
                return _browser;
            }
        }
        public WindowSelector WindowSelector
        {
            get
            {
                return new WindowSelector(_driver);
            }
        }
        public FrameSelector FrameSelector
        {
            get
            {
                return _frameSelector;
            }
        }
    }
}
