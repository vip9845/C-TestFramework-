//Copyright 2013 Eurofins Scientific Ltd, Ireland
//Usage reserved to Eurofins Global Franchise Model subscribers.

using OpenQA.Selenium;

namespace ETFSeleniumUtility2.Support
{
    public class FrameSelector
    {
        private readonly IWebDriver _driver;
        public FrameSelector(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SelectFrame(string frameName)
        {
            try
            {
                _driver.SwitchTo().Frame(frameName);
            }
            catch (NoSuchFrameException)
            {
                
                throw new NoSuchFrameException("There is no"+frameName+"in current page!");
            }
            
        }
        public void SelectFrame(IWebElement element)
        {
            try
            {
                _driver.SwitchTo().Frame(element);
            }
            catch (NoSuchFrameException)
            {

                throw new NoSuchFrameException("There is no" + element + "in current page!");
            }
            
        }
        public void SelectFrame(int frameIndex)
        {
            try
            {
                _driver.SwitchTo().Frame(frameIndex);
            }
            catch (NoSuchFrameException)
            {

                throw new NoSuchFrameException("There is no such frame in current page!");
            }
            
        }

    }
}
